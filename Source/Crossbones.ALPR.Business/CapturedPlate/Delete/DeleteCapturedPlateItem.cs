using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.CapturedPlate.Delete
{
    public class DeleteCapturedPlateItem : SysSerialItemMessage
    {
        public DeleteCapturedPlateItem(SysSerial id, DeleteCommandFilter deletdCommandFilter, long userId = 0) : base(id)
        {
            UserId = userId;
            DeletdCommandFilter = deletdCommandFilter;
        }

        public long UserId { get; }
        public DeleteCommandFilter DeletdCommandFilter { get; }
    }
}
