using Crossbones.Modules.Business.Common;

namespace Corssbones.ALPR.Business
{
    public class ALPRTenantsProvider : ITenantDbProvider
    {
        readonly IDictionary<string, string> _tenants;

        public ALPRTenantsProvider(Dictionary<string, string> cnnStr) => _tenants = cnnStr;

        public IDictionary<string, string> GetTenantDatabases() => _tenants;
    }
}