﻿using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;

namespace Corssbones.ALPR.Business.HotList.Get
{
    public class GetHotListItem : RecIdItemMessage
    {
        public GetHotListItem(RecId recId, GetQueryFilter filter) : base(recId)
        {
            QueryFilter = filter;
        }
        public Pager Paging { get; set; }
        public GetQueryFilter QueryFilter { get; set; }
        public GridSort Sort { get; set; }
        public GridFilter Filter { get; set; }

    }
}
