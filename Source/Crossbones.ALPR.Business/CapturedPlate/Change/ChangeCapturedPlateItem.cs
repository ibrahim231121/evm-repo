using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeCapturedPlateItem : SysSerialItemMessage
    {
        public ChangeCapturedPlateItem(SysSerial id, CapturedPlateItem updateItem) : base(id)
        {
            UpdateItem = updateItem;
        }

        public CapturedPlateItem UpdateItem { get; }
    }
}
