using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeUserCapturedPlateItem : RecIdItemMessage
    {
        public ChangeUserCapturedPlateItem(RecId recId, long userId, long capturedId) : base(recId)
        {
            UserId = userId;
            CapturedId = capturedId;
        }

        public long UserId { get; }
        public long CapturedId { get; }
    }
}
