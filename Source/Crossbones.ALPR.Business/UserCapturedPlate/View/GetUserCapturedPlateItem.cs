using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.CapturedPlate.Get
{
    public class GetUserCapturedPlateItem : SysSerialItemMessage
    {
        public GetUserCapturedPlateItem(long userId, SysSerial sysSerial, GetQueryFilter queryFilter) : base(sysSerial)
        {
            UserId = userId;
            QueryFilter = queryFilter;
        }

        public long UserId { get; }

        public GetQueryFilter QueryFilter { get; set; }

    }
}
