using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeCapturePlatesSummaryItem : RecIdItemMessage
    {
        public ChangeCapturePlatesSummaryItem(RecId id, CapturePlatesSummaryDTO updatedItem) : base(id)
        {
            UpdatedItem = updatedItem;
        }

        public CapturePlatesSummaryDTO UpdatedItem { get; }
    }
}
