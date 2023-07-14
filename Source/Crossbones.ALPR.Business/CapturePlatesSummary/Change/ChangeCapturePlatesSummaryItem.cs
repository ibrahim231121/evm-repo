using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeCapturePlatesSummaryItem : RecIdItemMessage
    {
        public ChangeCapturePlatesSummaryItem(RecId recId, CapturePlatesSummaryDTO updatedItem) : base(recId)
        {
            UpdatedItem = updatedItem;
        }

        public CapturePlatesSummaryDTO UpdatedItem { get; }
    }
}
