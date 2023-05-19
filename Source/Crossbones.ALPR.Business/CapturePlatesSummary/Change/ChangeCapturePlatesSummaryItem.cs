using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeCapturePlatesSummaryItem : CapturedPlateMessage
    {
        public ChangeCapturePlatesSummaryItem(SysSerial id, CapturePlatesSummaryItem updatedItem):base(id)
        {
            UpdatedItem = updatedItem;
        }

        public CapturePlatesSummaryItem UpdatedItem { get; }
    }
}
