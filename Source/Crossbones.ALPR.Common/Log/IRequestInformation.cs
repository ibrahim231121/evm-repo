
namespace Crossbones.ALPR.Common.Log
{
    public interface IRequestInformation
    {
        public string TenantServiceId { get; set; }
        public long TenantId { get; set; }
        public long UserId { get; set; }
    }
}
