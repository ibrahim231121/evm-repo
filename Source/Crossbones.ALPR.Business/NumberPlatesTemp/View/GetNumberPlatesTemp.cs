using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.NumberPlatesTemp.View
{
    public class GetNumberPlatesTemp : SysSerialItemMessage
    {
        public GetNumberPlatesTemp(SysSerial sysSerial, GetQueryFilter filter) : base(sysSerial)
        {
            Filter = filter;  
        }
        public Pager Paging { get; set; }
        public GetQueryFilter Filter { get; set; }
    }
}
