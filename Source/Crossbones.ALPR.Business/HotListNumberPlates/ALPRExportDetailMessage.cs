using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.HotListNumberPlates
{
    public class HotListNumberPlatesMessage : MessageBase
    {
        public SysSerial Id { get; }
        public HotListNumberPlatesMessage(SysSerial _id) => Id = _id;
    }
}
