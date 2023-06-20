using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddCapturePlatesSummaryStatusItem : RecIdItemMessage
    {
        public AddCapturePlatesSummaryStatusItem(RecId id, CapturePlatesSummaryStatusDTO itemToAdd) : base(id)
        {
            ItemToAdd = itemToAdd;
        }

        public CapturePlatesSummaryStatusDTO ItemToAdd { get; }
    }
}
