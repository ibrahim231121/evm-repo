using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;

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
