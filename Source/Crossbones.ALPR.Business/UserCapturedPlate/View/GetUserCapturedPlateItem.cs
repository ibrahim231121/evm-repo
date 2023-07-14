using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;

namespace Corssbones.ALPR.Business.CapturedPlate.Get
{
    public class GetUserCapturedPlateItem : RecIdItemMessage
    {
        public GetUserCapturedPlateItem(long userId, RecId recId, GetQueryFilter queryFilter) : base(recId)
        {
            UserId = userId;
            QueryFilter = queryFilter;
        }

        public long UserId { get; }

        public GetQueryFilter QueryFilter { get; set; }

    }
}
