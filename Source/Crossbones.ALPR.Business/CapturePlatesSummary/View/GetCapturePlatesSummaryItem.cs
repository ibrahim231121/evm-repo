using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;

namespace Corssbones.ALPR.Business.CapturedPlate.Get
{
    public class GetCapturePlatesSummaryItem : RecIdItemMessage
    {
        public GetCapturePlatesSummaryItem(long userId, RecId recId, long capturedPlateId, GetQueryFilter queryFilter, List<long> capturedPlateIds = null, GridFilter filter = null, Pager paging = null, GridSort sort = null) : base(recId)
        {
            Paging = paging;
            Filter = filter;
            Sort = sort;
            UserId = userId;
            CapturedPlateId = capturedPlateId;
            QueryFilter = queryFilter;
            CapturedPlateIds = capturedPlateIds;
        }

        public Pager Paging { get; }

        public GridSort Sort { get; }

        public GridFilter Filter { get; }

        public long UserId { get; }
        public long CapturedPlateId { get; }
        public GetQueryFilter QueryFilter { get; set; }
        public List<long> CapturedPlateIds { get; }
    }
}
