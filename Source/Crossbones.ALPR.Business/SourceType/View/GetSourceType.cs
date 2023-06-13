﻿using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;

namespace Corssbones.ALPR.Business.SourceType.View
{
    public class GetSourceType : SysSerialItemMessage
    {
        public GetSourceType(SysSerial sysSerial, GetQueryFilter filter) : base(sysSerial)
        {
            Filter = filter;
        }
        public Pager Paging { get; set; }
        public GetQueryFilter Filter { get; set; }

    }
}
