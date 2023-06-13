using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.CapturedPlate
{
    public class CapturedPlateMessage : MessageBase
    {
        public SysSerial Id { get; }
        public CapturedPlateMessage(SysSerial _id) => Id = _id;
    }
}
