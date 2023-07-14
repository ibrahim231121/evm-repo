using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddCapturePlatesSummaryItem : RecIdItemMessage
    {
        public AddCapturePlatesSummaryItem(RecId recId, CapturePlatesSummaryDTO itemToAdd) : base(recId)
        {
            ItemToAdd = itemToAdd;
        }

        public CapturePlatesSummaryDTO ItemToAdd { get; }
    }
}
