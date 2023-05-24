using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;

namespace Crossbones.ALPR.Business.NumberPlates
{
    public class NumberPlatesMessage: MessageBase
    {
        public SysSerial Id { get; }
        public NumberPlatesMessage(SysSerial _id)
            => Id = _id;
    }
}
