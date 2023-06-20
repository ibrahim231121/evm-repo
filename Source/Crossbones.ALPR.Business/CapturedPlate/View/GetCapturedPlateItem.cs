using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;

namespace Corssbones.ALPR.Business.CapturedPlate.Get
{
    public class GetCapturedPlateItem : RecIdItemMessage
    {
        public GetCapturedPlateItem(RecId sysSerial, GetQueryFilter queryFilter, List<long> capturedPlateIds = null, DateTime startDate = default, DateTime endDate = default, GridFilter filter = null, Pager paging = null, GridSort sort = null, List<long> hotListIds = null) : base(sysSerial)
        {
            Paging = paging;
            Filter = filter;
            Sort = sort;
            HotListIds = hotListIds;
            StartDate = startDate;
            EndDate = endDate;
            QueryFilter = queryFilter;
            CapturedPlateIds = capturedPlateIds;
        }

        public Pager Paging { get; }

        public GridSort Sort { get; }
        public List<long> HotListIds { get; }
        public GridFilter Filter { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public GetQueryFilter QueryFilter { get; set; }
        public List<long> CapturedPlateIds { get; }
    }
}
