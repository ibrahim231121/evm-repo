using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.CapturedPlate.Delete
{
    public class DeleteCapturePlatesSummaryStatusItem : RecIdItemMessage
    {
        public DeleteCapturePlatesSummaryStatusItem(RecId recId, DeleteCommandFilter deletdCommandFilter) : base(recId)
        {
            DeleteCommandFilter = deletdCommandFilter;
        }

        public DeleteCommandFilter DeleteCommandFilter { get; }
    }
}
