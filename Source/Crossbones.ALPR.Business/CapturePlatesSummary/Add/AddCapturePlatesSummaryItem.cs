using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddCapturePlatesSummaryItem : CapturedPlateMessage
    {
        public AddCapturePlatesSummaryItem(SysSerial id, CapturePlatesSummaryItem itemToAdd) : base(id)
        {
            ItemToAdd = itemToAdd;
        }

        public CapturePlatesSummaryItem ItemToAdd { get; }
    }
}
