using Corssbones.ALPR.Business;
using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;

namespace Crossbones.ALPR.Business.NumberPlates.Get
{
    public class GetNumberPlate : RecIdItemMessage
    {
        public GetNumberPlate(RecId recId, GetQueryFilter filter, GridFilter gridFilter, GridSort sort) : base(recId)
        {
            Filter = filter;
            GridFilter = gridFilter;
            Sort = sort;
        }

        public GetNumberPlate(RecId recId, GetQueryFilter filter) : base(recId)
        {
            Filter = filter;
        }

        public GetNumberPlate(RecId recId, GetQueryFilter filter, long hotListId) : base(recId)
        {
            Filter = filter;
            HotListID = hotListId;
        }

        public GetNumberPlate(RecId recId, GetQueryFilter filter, string numberPlate) : base(recId)
        {
            Filter = filter;
            NumberPlateString = numberPlate;
        }

        public Pager Paging { get; set; }
        public GetQueryFilter Filter { get; set; }
        public GridFilter GridFilter { get; set; }
        public GridSort Sort { get; }
        public long HotListID { get; set; }
        public string NumberPlateString { get; set; }
    }
}