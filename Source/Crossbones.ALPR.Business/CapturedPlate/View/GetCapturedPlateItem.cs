using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.CapturedPlate.View
{
    public class GetCapturedPlateItem : CapturedPlateMessage
    {        
        public GetCapturedPlateItem(SysSerial sysSerial, GetQueryFilter queryFilter, List<long> capturedPlateIds = null, DateTime startDate = default, DateTime endDate = default, GridFilter filter = null, Pager paging = null, GridSort sort = null) : base(sysSerial)
        {
            Paging = paging;
            Filter = filter;
            Sort = sort;
            StartDate = startDate;
            EndDate = endDate;
            QueryFilter = queryFilter;
            CapturedPlateIds = capturedPlateIds;
        }

        public Pager Paging { get; }

        public GridSort Sort { get; }

        public GridFilter Filter { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public GetQueryFilter QueryFilter { get; set; }
        public List<long> CapturedPlateIds { get; }
    }
}
