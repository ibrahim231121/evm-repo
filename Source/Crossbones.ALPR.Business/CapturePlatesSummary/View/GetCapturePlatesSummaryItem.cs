using Corssbones.ALPR.Business.Enums;
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
    public class GetCapturePlatesSummaryItem : CapturedPlateMessage
    {        
        public GetCapturePlatesSummaryItem(long userId, SysSerial sysSerial, long capturedPlateId, GetQueryFilter queryFilter, List<long> capturedPlateIds = null, GridFilter filter = null, Pager paging = null, GridSort sort = null) : base(sysSerial)
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
