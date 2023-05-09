using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.HotList
{
    public class HotListItemMessage : MessageBase
    {
        public SysSerial Id { get; }
        public HotListItemMessage(SysSerial _id) => Id = _id;

    }
}
