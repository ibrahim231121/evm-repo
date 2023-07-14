using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.NumberPlatesTemp.Delete
{
    public class DeleteNumberPlatesTemp : RecIdItemMessage
    {
        public DeleteNumberPlatesTemp(RecId recId) : base(recId) { }
    }
}