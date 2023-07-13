using Crossbones.ALPR.Common.Log;
using Crossbones.Modules.Business;

namespace Corssbones.ALPR.Business
{
    public class MessageBase : IDbRequest, IRequestInformation
    {
        public long UserId { get; set; }
        public string TenantServiceId { get; set; }
        public long TenantId { get; set; }
    }
}
