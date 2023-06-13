using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeCapturePlatesSummaryItem : SysSerialItemMessage
    {
        public ChangeCapturePlatesSummaryItem(SysSerial id, CapturePlatesSummaryItem updatedItem) : base(id)
        {
            UpdatedItem = updatedItem;
        }

        public CapturePlatesSummaryItem UpdatedItem { get; }
    }
}
