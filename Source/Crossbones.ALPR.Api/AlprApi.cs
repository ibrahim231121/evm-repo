﻿using AutoMapper;
using Corssbones.ALPR.Business;
using Crossbones.ALPR.Api.AlprSearch.Service;
using Crossbones.ALPR.Api.CapturedPlate;
using Crossbones.ALPR.Api.CapturePlatesSummary;
using Crossbones.ALPR.Api.CapturePlatesSummaryStatus;
using Crossbones.ALPR.Api.ExportDetails;
using Crossbones.ALPR.Api.HotList.Service;
using Crossbones.ALPR.Api.HotListDataSource.Service;
using Crossbones.ALPR.Api.HotListDataSourceMapping.Service;
using Crossbones.ALPR.Api.HotListNumberPlates;
using Crossbones.ALPR.Api.HotListSourceType.Service;
using Crossbones.ALPR.Api.NumberPlates.Service;
using Crossbones.ALPR.Api.NumberPlatesTemp.Service;
using Crossbones.ALPR.Api.State;
using Crossbones.ALPR.Common.Log;
using Crossbones.ALPR.Common.ServiceConfiguration;
using Crossbones.Common;
using Crossbones.Modules.Api;
using Crossbones.Modules.Api.Middlewares;
using Crossbones.Modules.Business.Common;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Configuration;
using Crossbones.Modules.Common.Exceptions;
using Crossbones.Modules.Common.Lazy;
using Crossbones.Modules.Common.ServiceDiscovery;
using Crossbones.Modules.Common.ServiceDiscovery.Helper;
using Crossbones.Modules.Sequence.Common.Interfaces;
using Crossbones.Modules.Sequence.Common.Proxy;
using Crossbones.Transport.Pipes;
using Crossbones.Workers;
using Crossbones.Workers.Common;
using Hangfire;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Net;

namespace Crossbones.ALPR.Api
{
    [Worker("ALPR Micro Service API"), Description("EVM 4.0")]
    public sealed class AlprApiManifest : IWorkerManifest
    {
        public void Register(IWorkerManifestRegistrant registrant)
        {
            registrant
                .WorkerIs<AlprApi>(WorkerType.Active)
                .WithPolicy(ExecutionPolicy.Default);
        }
    }

    public class AlprApi : ApiWorker<AlprApi>
    {
        readonly ALPRServiceConfiguration _configuration;

        readonly ApiConfiguration _apiConfiguration;
        readonly WebServer _serverConfiguration;
        readonly string cors = "*";

        readonly ITenantDbProvider _tenantDbProvider;
        IServiceCollection _services;

        public AlprApi(ITenantDbProvider tenantDbProvider)
        {
            _configuration = FileConfiguration.Read<ALPRServiceConfiguration>(new[] { @"Configuration.json" });
            _apiConfiguration = _configuration.ApiConfiguration;
            _serverConfiguration = _configuration.WebServer;
            _tenantDbProvider = tenantDbProvider;
        }

        protected override Task Run(CancellationToken token)
        {
            var _task = base.Run(token);

            var serviceProvider = _services.BuildServiceProvider();

            var mappingService = serviceProvider.GetService<IHotListDataSourceMappingService>();
            mappingService!.EnqueJobAllDataSources();

            Console.WriteLine($"Job {HotListDataSourceMappingService.RECURRING_JOB_NAME} enqueued successfully");

            return _task;
        }

        protected override void RegisterDependencies(IServiceCollection services)
        {
            _services = services;
            base.RegisterDependencies(services);

            GlobalConfiguration.Configuration
                //.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                //.UseColouredConsoleLogProvider()
                //.UseSimpleAssemblyNameTypeSerializer()
                //.UseRecommendedSerializerSettings()
                .UseSqlServerStorage(_configuration.HangfireConnection);

            services.AddSingleton<IServiceDiscoveryConfiguration>(_configuration);

            services.Add(new ServiceDescriptor(typeof(ISequenceProxyFactory), typeof(SequenceProxyProvider), ServiceLifetime.Scoped));
            services.AddScoped<ServiceArguments>();

            services.Add(new ServiceDescriptor(typeof(IHotListService), typeof(HotListService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IExportDetailService), typeof(ExportDetailService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IHotListNumberPlateService), typeof(HotListNumberPlateService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IHotListDataSourceItemService), typeof(HotListDataSourceItemService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IHotListDataSourceMappingService), typeof(HotListDataSourceMappingService), ServiceLifetime.Transient));

            services.Add(new ServiceDescriptor(typeof(ISourceTypeService), typeof(SourceTypeService), ServiceLifetime.Transient));

            services.AddScoped<ICapturedPlateService, CapturedPlateService>();
            services.AddScoped<ICapturePlatesSummaryService, CapturePlatesSummaryService>();
            services.AddScoped<ICapturePlatesSummaryStatusService, CapturePlatesSummaryStatusService>();
            services.AddScoped<IUserCapturedPlateService, UserCapturedPlateService>();
            services.Add(new ServiceDescriptor(typeof(INumberPlateService), typeof(NumberPlateService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(INumberPlatesTempService), typeof(NumberPlatesTempService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IStateService), typeof(StateService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(ISearchService), typeof(SearchService), ServiceLifetime.Transient));

            services.AddSingleton<IMessageChannel>(_channel);
            services.AddSingleton<IMemoryCache, MemoryCache>();

            services.AddSingleton(typeof(ServiceType), ServiceType.ALPR);
            services.AddLazyResolution();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            });


            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ALPRMappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton<IMapper>(mapper);
        }

        protected override void Setup(CancellationToken token)
        {
            Configure(x =>
            {
                x.name = ServiceType.ALPR.ToString();
                x.CORSPolicies = new Dictionary<string, Action<CorsPolicyBuilder>>()
                {
                    {
                        "policy1", (x) =>
                        {
                            x.WithOrigins(cors).AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                        }
                    }
                };
                x.Environment = (FrameworkEnvironment)_apiConfiguration.Enviorment;
                x.ReuseStrings = _apiConfiguration.ReuseStrings;
                x.ListensAt = new IPEndPoint(IPAddress.Parse(_serverConfiguration.IP), _serverConfiguration.Port);
                x.EnableExternalListening = _apiConfiguration.EnableExternalListening;
                x.HttpVersion = (Crossbones.Modules.Api.HttpVersion)_apiConfiguration.HttpVersion;
                x.AddExceptionMiddleware<ExeceptionHandling>(ex =>
                {
                    Log.Error(LogRequest.GetLogMessage(_services.BuildServiceProvider().GetService<IHttpContextAccessorTenant>().HttpContext.Request, ex.Message));
                    return ex switch
                    {
                        RecordNotFound e => ExceptionsHelper.GetStatusCodeOfException(e),
                        CannotBeEmpty e => ExceptionsHelper.GetStatusCodeOfException(e),
                        MustBeEmpty e => ExceptionsHelper.GetStatusCodeOfException(e),
                        CheckFailed e => ExceptionsHelper.GetStatusCodeOfException(e),
                        StaleDataModified e => ExceptionsHelper.GetStatusCodeOfException(e),
                        ModificationNotAllowed e => ExceptionsHelper.GetStatusCodeOfException(e),
                        DuplicationNotAllowed e => ExceptionsHelper.GetStatusCodeOfException(e),
                        OperationNotAllowed e => ExceptionsHelper.GetStatusCodeOfException(e),
                        DeleteNotAllowed e => ExceptionsHelper.GetStatusCodeOfException(e),
                        ValidationFailed e => ExceptionsHelper.GetStatusCodeOfException(e),
                        InvalidValue e => ExceptionsHelper.GetStatusCodeOfException(e),
                        InaccessibleRecord e => HttpStatusCode.Gone,
                        Unauthorized e => HttpStatusCode.Unauthorized,
                        _ => 0
                    };
                });
                x.userServiceUrl = ConsulHelper.UsersIdentities(_configuration) + "modules/InternalModules";
                x.TenantId = _tenantDbProvider.GetTenantDatabases().Keys.Count > 0 ? _tenantDbProvider.GetTenantDatabases().Keys.FirstOrDefault().ToString() : "0";
            });
            base.Setup(token);
        }
    }
}