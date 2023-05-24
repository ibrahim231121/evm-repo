using Crossbones.ALPR.Api.ALPREvents;
using Crossbones.ALPR.Api.ALPREvents.ExportDetails;
using Crossbones.ALPR.Api.ALPREvents.HotListNumberPlates;
using Crossbones.ALPR.Api.CapturedPlate;
using Crossbones.ALPR.Api.CapturePlatesSummary;
using Crossbones.ALPR.Api.CapturePlatesSummaryStatus;
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
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph.SecurityNamespace;
using System.ComponentModel;
using System.Net;


namespace Crossbones.ALPR.Api
{
    [Worker("ALPR Micro Service API"), Description("EVM 4.0")]
    public sealed class ALPRApiManifest : IWorkerManifest
    {
        public void Register(IWorkerManifestRegistrant registrant)
        {
            registrant
                .WorkerIs<ALPRApi>(WorkerType.Active)
                .WithPolicy(ExecutionPolicy.Default);
        }
    }

    public class ALPRApi : ApiWorker<ALPRApi>
    {
        readonly ALPRServiceConfiguration _configuration;

        readonly ApiConfiguration _apiConfiguration;
        readonly WebServer _serverConfiguration;
        readonly string cors = "*";

        readonly ITenantDbProvider _tenantDbProvider;

        public ALPRApi(ITenantDbProvider tenantDbProvider)
        {
            _configuration = FileConfiguration.Read<ALPRServiceConfiguration>(new[] { @"Configuration.json" });
            _apiConfiguration = _configuration.ApiConfiguration;
            _serverConfiguration = _configuration.WebServer;
            _tenantDbProvider = tenantDbProvider;
        }

        protected override void RegisterDependencies(IServiceCollection services)
        {
            base.RegisterDependencies(services);
            services.Add(new ServiceDescriptor(typeof(ISequenceProxyFactory), typeof(SequenceProxyProvider), ServiceLifetime.Scoped));
            services.AddScoped<ServiceArguments>();

            services.Add(new ServiceDescriptor(typeof(IHotListItemService), typeof(HotListItemService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IExportDetailService), typeof(ExportDetailService), ServiceLifetime.Transient));
            services.Add(new ServiceDescriptor(typeof(IHotListNumberPlateService), typeof(HotListNumberPlateService), ServiceLifetime.Transient));

            services.AddScoped<ICapturedPlateService, CapturedPlateService>();
            services.AddScoped<ICapturePlatesSummaryService, CapturePlatesSummaryService>();
            services.AddScoped<ICapturePlatesSummaryStatusService, CapturePlatesSummaryStatusService>();
            services.AddScoped<IUserCapturedPlateService, UserCapturedPlateService>();

            services.AddSingleton<IMessageChannel>(_channel);

            services.AddSingleton(typeof(ServiceType), ServiceType.ALPR);
            services.AddLazyResolution();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            });
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