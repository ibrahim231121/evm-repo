using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeCapturePlatesSummaryStatusItem : RecIdItemMessage
    {
        public ChangeCapturePlatesSummaryStatusItem(RecId id, CapturePlatesSummaryStatusDTO updatedItem) : base(id)
        {
            UpdatedItem = updatedItem;
        }

        public CapturePlatesSummaryStatusDTO UpdatedItem { get; }
    }
}
