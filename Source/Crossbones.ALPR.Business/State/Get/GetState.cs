using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;

namespace Crossbones.ALPR.Business.State.Get
{
    public class GetState : RecIdItemMessage
    {
        public GetState(RecId _id) : base(_id)
        {
        }
    }
}
