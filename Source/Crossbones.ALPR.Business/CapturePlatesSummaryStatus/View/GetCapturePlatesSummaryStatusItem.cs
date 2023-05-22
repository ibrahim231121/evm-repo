﻿using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.CapturedPlate.Get
{
    public class GetCapturePlatesSummaryStatusItem : CapturedPlateMessage
    {        
        public GetCapturePlatesSummaryStatusItem(SysSerial sysSerial, GetQueryFilter queryFilter, GridFilter filter = null, Pager paging = null, GridSort sort = null) : base(sysSerial)
        {
            Paging = paging;
            Filter = filter;
            Sort = sort;
            QueryFilter = queryFilter;
        }

        public Pager Paging { get; }

        public GridSort Sort { get; }

        public GridFilter Filter { get; }

        public GetQueryFilter QueryFilter { get; set; }

    }
}
