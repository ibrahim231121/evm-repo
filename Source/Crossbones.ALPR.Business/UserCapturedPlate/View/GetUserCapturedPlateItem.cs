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
    public class GetUserCapturedPlateItem : CapturedPlateMessage
    {        
        public GetUserCapturedPlateItem(long userId, SysSerial sysSerial, GetQueryFilter queryFilter) : base(sysSerial)
        {
            UserId = userId;
            QueryFilter = queryFilter;
        }

        public long UserId { get; }

        public GetQueryFilter QueryFilter { get; set; }

    }
}
