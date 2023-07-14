using Corssbones.ALPR.Business;
using Crossbones.ALPR.Common.ValueObjects;

namespace Crossbones.ALPR.Business.NumberPlates.Delete
{
    public class DeleteNumberPlate : RecIdItemMessage
    {
        public DeleteNumberPlate(RecId recId) : base(recId)
        {

        }
    }
}
