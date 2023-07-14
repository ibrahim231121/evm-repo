using Corssbones.ALPR.Business;
using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;

namespace Crossbones.ALPR.Business.NumberPlates.Get
{
    public class GetNumberPlate : RecIdItemMessage
    {
        //public GetNumberPlate(SysSerial sysSerial, GetQueryFilter filter, GridFilter gridFilter, GridSort sort) : base(sysSerial)
        //{
        //    Filter = filter;
        //    GridFilter = gridFilter;
        //    Sort = sort;
        //}

        public GetNumberPlate(RecId recId, GetQueryFilter filter) : base(recId)
        {
            Filter = filter;
        }

        public GetNumberPlate(RecId recId, GetQueryFilter filter, long hotListId) : base(recId)
        {
            Filter = filter;
            HotListID = hotListId;
        }

        public Pager Paging { get; set; }
        public GetQueryFilter Filter { get; set; }
        public GridFilter GridFilter { get; set; }
        public GridSort Sort { get; set; }
        public long HotListID { get; set; }
    }
}
