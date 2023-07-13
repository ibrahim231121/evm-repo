using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.CapturedPlate.Delete
{
    public class DeleteUserCapturedPlateItem : RecIdItemMessage
    {
        public DeleteUserCapturedPlateItem(RecId id, DeleteCommandFilter deletdCommandFilter, long userId = 0, long capturedPlateId = 0) : base(id)
        {
            UserId = userId;
            CapturedPlateId = capturedPlateId;
            DeletdCommandFilter = deletdCommandFilter;
        }

        public long UserId { get; }
        public long CapturedPlateId { get; }
        public DeleteCommandFilter DeletdCommandFilter { get; }
    }
}
