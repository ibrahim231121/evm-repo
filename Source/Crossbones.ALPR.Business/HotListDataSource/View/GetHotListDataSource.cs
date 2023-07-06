﻿using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;

namespace Corssbones.ALPR.Business.HotListDataSource.View
{
    public class GetHotListDataSource : RecIdItemMessage
    {
        public GetHotListDataSource(RecId recId, GetQueryFilter filter, GridFilter gridFilter, GridSort sort) : base(recId)
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
