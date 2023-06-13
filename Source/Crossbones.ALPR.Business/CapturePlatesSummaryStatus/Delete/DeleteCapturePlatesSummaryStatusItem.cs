using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.CapturedPlate.Delete
{
    public class DeleteCapturePlatesSummaryStatusItem : SysSerialItemMessage
    {
        public DeleteCapturePlatesSummaryStatusItem(SysSerial id, DeleteCommandFilter deletdCommandFilter) : base(id)
        {
            DeleteCommandFilter = deletdCommandFilter;
        }

        public DeleteCommandFilter DeleteCommandFilter { get; }
    }
}
