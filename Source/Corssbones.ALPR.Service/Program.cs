using Corssbones.ALPR.Business;
using Crossbones.ALPR.Api;
using Crossbones.ALPR.Common.ServiceConfiguration;
using Crossbones.Common.DependencyInjection;
using Crossbones.DependencyInjection.SimpleInjector;
using Crossbones.Logging;
using Crossbones.Logging.Display;
using Crossbones.Logging.Nlog;
using Crossbones.Microkernel;
using Crossbones.Modules.Business.Common;
using Crossbones.Modules.Common.Configuration;
using Crossbones.Modules.Common.ServiceDiscovery;
using Crossbones.Modules.Logger;
using Crossbones.Modules.Sequence;
using Crossbones.Modules.Sequence.Common.Configuration;
using Crossbones.Modules.Vault.Configuration;
using Crossbones.Modules.Vault.VaultService;
using Crossbones.Transport.InMemory;
using System.Reflection;

namespace Corssbones.ALPR.Service
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Step 1: Define your configuration files and read them. Although you can read multiple files, of JSON and XML,
            // try not misusing the feature by ONLY specifying the files that actually exist. that is, if you do not  
            // use an XML file, there is absolutely NO POINT mentioning one in the files.  
            var files = new[] { @"Configuration.json" };
            var configuration = FileConfiguration.Read<ALPRServiceConfiguration>(files);
            configuration.Name = ServiceType.ALPR.ToString();
            var logPathInfo = (logFileName: DateTime.UtcNow.ToString("MMddyyyy"), rootDir: AppDomain.CurrentDomain.BaseDirectory);
            //configuration.VaultConfiguration.ServiceType = (byte)ServiceType.ALPR;

            // Step 2: Get your configuration DB string. You will need it to get tenants info  
            var configurationDatabaseConnection = configuration.ConfigurationDatabases!.FirstOrDefault();
            // Code below is completely OPTIONAL. No need if you are sure this eats up processing time.
            // OR have it in DEVELOPMENT but disable in PRODUCTION. or Don't use it at all.
#if DEBUG
            if (!await Utility.VerifyConnection(configurationDatabaseConnection))
                throw new Exception("Please provide valid configuration to connect to the configuration database");
#endif


            // Step 3: Setup configuration for microkernel. For all practical purposes, default settings work fine.
            // Configure these settings, only if you have special needs to tweak how the microkernel work. 
            // if you do not have any special needs, AVOID TWEAKING Micro kernel settings. WRONG settings can adversely affect performance.
            var config = new KernelConfiguration(new SimpleInjectorContainer())
            {
                MessageQueueCapacityKB = configuration.MessageQueueCapacity,
                RouterMemoryInMB = configuration.RouterMemoryInMB,
                RequestTimeOut = TimeSpan.FromMinutes(configuration.RequestTimeOut),
                LogFormat = new DefaultFormat
                {
                    MessageLength = configuration.LogFormat.ExceptionLength,
                    TextLength = configuration.LogFormat.TextLength,
                    ExceptionLength = configuration.LogFormat.ExceptionLength
                },
                FolderLocation = new Crossbones.Microkernel.Structures.FolderLocation(
                    String.Empty, String.Empty, String.Empty,
                    Path.Combine(logPathInfo.rootDir, "Logs", $"{logPathInfo.logFileName}.log"))
            };

            //var mappingConfig = new MapperConfiguration(mc =>
            //{
            //    mc.AddProfile(new MappingProfile());
            //});

            //var mapper = mappingConfig.CreateMapper();
            //services.AddSingleton<IMapper>(mapper);


            // Step 4:Define log writers and pipe type
            //configuration.FluentdConfiguration.Tag = ServiceType.ALPR.ToString();
            var nLogWriter = new NlogWriter(new NlogConfiguration(config.LogFormat, config.FolderLocation.logging, configuration.FluentdConfiguration, ServiceType.ALPR.ToString()));
            nLogWriter.Info("ALPR micro-service is starting...!", DateTime.UtcNow);
            nLogWriter.Info($"Assembly file version {Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion}", DateTime.UtcNow);
            nLogWriter.Info($"Assembly Version {Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version}", DateTime.UtcNow);
            config.PipeCreate = (pipe) => new InMemoryPipe(pipe);
            config.PipeType = typeof(InMemoryPipe);
            config.LogCreaters.Add((fmt, path) => nLogWriter);
            config.LogCreaters.Add((fmt, path) => new DisplayWriter(fmt));

            ILogWriter[] logWriters = config.LogCreaters.Select(func => func(config.LogFormat, config.FolderLocation)).ToArray();

            var log = new SafeLogger(logWriters);

            string serviceId = await ConsulService.Register(configuration, nLogWriter);

            configuration.MQTTConfiguration.ClientId = $"{configuration.Name}_{serviceId}";
            log.Info($"Registering service on MQTTBroker with CliendId: {configuration.MQTTConfiguration.ClientId}");

            IDictionary<string, string> t0;

            if (configuration.VaultConfiguration != null && configuration.VaultConfiguration.Status)
            {
                var vault = new VaultService(new VaultConfigurationProvider(configuration.VaultConfiguration));
                t0 = vault.Read((byte)ServiceType.ALPR);
            }
            else
            {
                t0 = await TenantConfiguration.Read(configurationDatabaseConnection, log, ServiceType.ALPR);
            }


            // Step 5: Get your tenants info from configuration. You config DB should have this info.
            // Register your ITenantDbProvider implementation
            //var t0 = await TenantConfiguration.Read(configurationDatabaseConnection, log, ServiceType.ALPR);

            var t1 = t0!.ToDictionary(x => x.Key, x => x.Value);

            var tdp = new ALPRTenantsProvider(t1);

            config.Register(() => config, LifeSpan.Singleton);
            config.Register<SafeLogger>(() => log, LifeSpan.Singleton);
            config.Register<ITenantDbProvider>(() => tdp, LifeSpan.Singleton);
            //config.Register<MQTT_Configuration>(() => configuration.MQTTConfiguration, LifeSpan.Singleton);
            config.Register<IVaultConfiguration>(() => new VaultConfigurationProvider(configuration.VaultConfiguration), LifeSpan.Singleton);
            config.Register<SequenceConfiguration>(() => new SequenceConfiguration(5000, 10000, ALPRResources.HotList), LifeSpan.Singleton);
            //config.Register<SequenceConfiguration>(() => new SequenceConfiguration(5000, 10000, ALPRResources.ExortDetail), LifeSpan.Singleton);

            //List your workers. you need to provide this list to micro kernel. 
            var loader = new ManifestReader(
                typeof(AlprApiManifest),
                typeof(SequencerManifest),
                typeof(Logger),
                typeof(ALPRBusinessManifest)
                );

            AppDomain.CurrentDomain.ProcessExit += async (s, ev) =>
            {
                await ConsulService.Deregister(serviceId, configuration, nLogWriter);
            };
            Console.CancelKeyPress += async (s, ev) =>
            {
                await ConsulService.Deregister(serviceId, configuration, nLogWriter);
            };

            // Start your micro kernel. if everything is correct, it should run fine at this stage
            using (var kernel = new Kernel<InMemoryPipe>(config, loader, (x) => new InMemoryPipe(x)))
            {
                var cts = new CancellationTokenSource();
                kernel.Run(cts.Token);
            }

            nLogWriter.Info("ALPR micro-service has stopped.", DateTime.UtcNow);
        }
    }
}