using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.CapturedPlate.Delete
{
    public class DeleteCapturePlatesSummaryItem : CapturedPlateMessage
    {
        public DeleteCapturePlatesSummaryItem(SysSerial id, DeleteCommandFilter deletdCommandFilter, long userId = 0, long capturedPlateId = 0) : base(id)
        {
            UserId = userId;
            CapturedPlateId = capturedPlateId;
            DeletdCommandFilter = deletdCommandFilter;
        }

        public long UserId { get; }
        public long CapturedPlateId { get; }
        public DeleteCommandFilter DeletdCommandFilter { get; }
    }
}
