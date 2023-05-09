using Crossbones.Common.DependencyInjection;
using Crossbones.Modules.Business;
using Crossbones.Modules.Business.Common;
using System.Reflection;

namespace Corssbones.ALPR.Business
{
    public class ALPR_Business { }
    public class ALPRBusinessManifest : BusinessManifest<Business<ALPR_Business>>
    {
        protected override void Configure(BusinessConfiguration<Business<ALPR_Business>> config)
        {
            //base.Configure(config);
            config.EnableLogging = true;
            config.ShowDbException = true;
            base.Configure(config);
        }
        protected override IEnumerable<Assembly> AddAssemblies() => new[] { typeof(ALPR_Business).Assembly };
        protected override void RegisterDependencies(IDependencyRegistrar reg)
        {
            reg.Register<IDbContextCreator>(() => new ALPRContextCreator(), LifeSpan.Scoped);
            base.RegisterDependencies(reg);
        }
    }
}

