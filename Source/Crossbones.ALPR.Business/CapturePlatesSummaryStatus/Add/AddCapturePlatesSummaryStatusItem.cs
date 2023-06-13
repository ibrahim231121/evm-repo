using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddCapturePlatesSummaryStatusItem : SysSerialItemMessage
    {
        public AddCapturePlatesSummaryStatusItem(SysSerial id, CapturePlatesSummaryStatusItem itemToAdd) : base(id)
        {
            ItemToAdd = itemToAdd;
        }

        public CapturePlatesSummaryStatusItem ItemToAdd { get; }
    }
}
