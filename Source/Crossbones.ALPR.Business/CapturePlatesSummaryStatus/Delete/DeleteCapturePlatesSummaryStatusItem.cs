using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.CapturedPlate.Delete
{
    public class DeleteCapturePlatesSummaryStatusItem : RecIdItemMessage
    {
        public DeleteCapturePlatesSummaryStatusItem(RecId id, DeleteCommandFilter deletdCommandFilter) : base(id)
        {
            DeleteCommandFilter = deletdCommandFilter;
        }

        public DeleteCommandFilter DeleteCommandFilter { get; }
    }
}
