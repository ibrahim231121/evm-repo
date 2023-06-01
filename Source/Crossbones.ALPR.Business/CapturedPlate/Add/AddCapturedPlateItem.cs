using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddCapturedPlateItem : SysSerialItemMessage
    {
        public AddCapturedPlateItem(SysSerial id, CapturedPlateItem capturedPlateItem) : base(id)
        {
            CapturedPlateItem = capturedPlateItem;
        }

        public CapturedPlateItem CapturedPlateItem { get; }
    }
}
