using Crossbones.ALPR.Common.Log;
using Crossbones.Modules.Business;

namespace Corssbones.ALPR.Business
{
    public class MessageBase : IDbRequest, IRequestInformation
    {
        public string TenantServiceId { get; set; }
    }
}
