using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeCapturePlatesSummaryStatusItem : RecIdItemMessage
    {
        public ChangeCapturePlatesSummaryStatusItem(RecId recId, CapturePlatesSummaryStatusDTO updatedItem) : base(recId)
        {
            UpdatedItem = updatedItem;
        }

        public CapturePlatesSummaryStatusDTO UpdatedItem { get; }
    }
}
