using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeCapturePlatesSummaryStatusItem : SysSerialItemMessage
    {
        public ChangeCapturePlatesSummaryStatusItem(SysSerial id, CapturePlatesSummaryStatusItem updatedItem) : base(id)
        {
            UpdatedItem = updatedItem;
        }

        public CapturePlatesSummaryStatusItem UpdatedItem { get; }
    }
}
