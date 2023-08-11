﻿using Crossbones.Modules.Cache.Common.Configuration;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Configuration;
using Crossbones.Modules.Common.Fluentd;
using Crossbones.Modules.Common.ServiceDiscovery;
using Crossbones.Modules.Vault.Configuration;

namespace Crossbones.ALPR.Common.ServiceConfiguration
{
    public class ALPRServiceConfiguration : IServiceDiscoveryConfiguration
    {
        public int MessageQueueCapacity { get; set; }
        public int RouterMemoryInMB { get; set; }
        public int RequestTimeOut { get; set; }
        public RedisConfiguration RedisConfiguration { get; set; }
        public DatabaseConfiguration DatabaseConfiguration { get; set; }
        public string HangfireConnection { get; set; }
        public string Language { get; set; }
        public LogFormat LogFormat { get; set; }
        public ApiConfiguration ApiConfiguration { get; set; }
        public string Name { get; set; }
        public ConsulServiceConfiguration ConsulServiceConfiguration { get; set; }
        public WebServer WebServer { get; set; }
        public string SecretKey { get; set; }
        public Services ServiceURLs { get; set; }
        public VaultConfiguration VaultConfiguration { get; set; }
        public bool VaultIntegration { get; set; }
        public FluentdConfiguration FluentdConfiguration { get; set; }
        public string FFmpegLoaderPath { get; set; }
        public GrpcServerConfiguration GrpcServerConfiguration { get; set; }
    }

    public class LogFormat
    {
        public int ExceptionLength { get; set; }
        public int MessageLength { get; set; }
        public int TextLength { get; set; }
    }

    public sealed class Password
    {
        public int Iteration { get; set; }
    }

    public sealed class BusinessConfiguration
    {
        public int ContextPoolSize { get; set; }
        public bool EnableLogging { get; set; }
        public bool ShowDbException { get; set; }
        public int RequestMaxProcessingTime { get; set; }
        public bool RequestMonitoring { get; set; }

    }
    public class TemporaryTenants
    {
        public long Authentication { get; set; }
        public long Users { get; set; }
        public long Configuration { get; set; }
        public long Units { get; set; }
    }
}