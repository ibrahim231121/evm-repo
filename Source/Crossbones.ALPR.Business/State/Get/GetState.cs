using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;

namespace Crossbones.ALPR.Business.State.Get
{
    public class GetState : SysSerialItemMessage
    {
        public GetState(SysSerial _id) : base(_id)
        {
        }
    }
}
