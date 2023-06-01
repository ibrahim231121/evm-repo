using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeCapturedPlateItem: SysSerialItemMessage
    {
        public ChangeCapturedPlateItem(SysSerial id, CapturedPlateItem updateItem):base(id)
        {
            UpdateItem = updateItem;
        }

        public CapturedPlateItem UpdateItem { get; }
    }
}
