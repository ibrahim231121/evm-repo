using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;

namespace Corssbones.ALPR.Business.HotListDataSource.View
{
    public class GetHotListDataSource : SysSerialItemMessage
    {
        public GetHotListDataSource(SysSerial sysSerial, GetQueryFilter filter, GridFilter gridFilter, GridSort sort) : base(sysSerial)
        {
            Filter = filter;
            GridFilter = gridFilter;
            Sort = sort;
        }

        public Pager Paging { get; set; }
        public GetQueryFilter Filter { get; set; }
        public GridFilter GridFilter { get; set; }
        public GridSort Sort { get; }

    }
}
