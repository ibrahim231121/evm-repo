using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business
{
    public class SysSerialItemMessage : MessageBase
    {
        public SysSerial Id { get; }
        public SysSerialItemMessage(SysSerial _id) => Id = _id;
    }
}