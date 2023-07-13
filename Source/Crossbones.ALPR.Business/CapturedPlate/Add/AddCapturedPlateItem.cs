using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddCapturedPlateItem : RecIdItemMessage
    {
        public AddCapturedPlateItem(RecId id, CapturedPlateDTO capturedPlateItem) : base(id)
        {
            CapturedPlateItem = capturedPlateItem;
        }

        public CapturedPlateDTO CapturedPlateItem { get; }
    }
}
