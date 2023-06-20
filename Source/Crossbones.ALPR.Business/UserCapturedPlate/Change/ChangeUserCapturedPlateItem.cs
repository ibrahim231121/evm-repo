using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.CapturedPlate.Change
{
    public class ChangeUserCapturedPlateItem : RecIdItemMessage
    {
        public ChangeUserCapturedPlateItem(RecId id, long userId, long capturedId) : base(id)
        {
            UserId = userId;
            CapturedId = capturedId;
        }

        public long UserId { get; }
        public long CapturedId { get; }
    }
}
