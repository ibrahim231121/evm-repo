using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddUserCapturedPlateItem : SysSerialItemMessage
    {
        public AddUserCapturedPlateItem(SysSerial id, long userId, long capturedId) : base(id)
        {
            UserId = userId;
            CapturedId = capturedId;
        }

        public long UserId { get; }
        public long CapturedId { get; }
    }
}
