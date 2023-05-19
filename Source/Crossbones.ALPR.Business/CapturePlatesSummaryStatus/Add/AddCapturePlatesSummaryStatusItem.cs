using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddCapturePlatesSummaryStatusItem : CapturedPlateMessage
    {
        public AddCapturePlatesSummaryStatusItem(SysSerial id, CapturePlatesSummaryStatusItem itemToAdd) : base(id)
        {
            ItemToAdd = itemToAdd;
        }

        public CapturePlatesSummaryStatusItem ItemToAdd { get; }
    }
}
