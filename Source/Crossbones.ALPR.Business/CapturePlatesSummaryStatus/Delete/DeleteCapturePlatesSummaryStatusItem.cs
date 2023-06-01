using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
