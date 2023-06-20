using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.CapturedPlate.Add
{
    public class AddUserCapturedPlateItem : RecIdItemMessage
    {
        public AddUserCapturedPlateItem(RecId id, long userId, long capturedId) : base(id)
        {
            UserId = userId;
            CapturedId = capturedId;
        }

        public long UserId { get; }
        public long CapturedId { get; }
    }
}
