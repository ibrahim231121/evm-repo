using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddCapturePlatesSummaryItem : SysSerialItemMessage
    {
        public AddCapturePlatesSummaryItem(SysSerial id, CapturePlatesSummaryItem itemToAdd) : base(id)
        {
            ItemToAdd = itemToAdd;
        }

        public CapturePlatesSummaryItem ItemToAdd { get; }
    }
}
