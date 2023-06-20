using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;

namespace Corssbones.ALPR.Business.CapturedPlate.Get
{
    public class GetCapturedPlateItem : RecIdItemMessage
    {        
        public GetCapturedPlateItem(RecId sysSerial, GetQueryFilter queryFilter, long userId = 0, DateTime startDate = default, DateTime endDate = default, GridFilter filter = null, Pager paging = null, GridSort sort = null, long hotListId = 0) : base(sysSerial)
        {
            Paging = paging;
            Filter = filter;
            Sort = sort;
            HotListId = hotListId;
            StartDate = startDate;
            EndDate = endDate;
            QueryFilter = queryFilter;
            UserId = userId;
        }

        public Pager Paging { get; }

        public GridSort Sort { get; }
        public long HotListId { get; }
        public GridFilter Filter { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public GetQueryFilter QueryFilter { get; set; }
        public long UserId { get; }
    }
}
