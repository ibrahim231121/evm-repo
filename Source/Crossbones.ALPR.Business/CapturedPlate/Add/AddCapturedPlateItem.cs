using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddCapturedPlateItem : RecIdItemMessage
    {
        public AddCapturedPlateItem(RecId recId, CapturedPlateDTO capturedPlateItem) : base(recId)
        {
            CapturedPlateItem = capturedPlateItem;
        }

        public CapturedPlateDTO CapturedPlateItem { get; }
    }
}
