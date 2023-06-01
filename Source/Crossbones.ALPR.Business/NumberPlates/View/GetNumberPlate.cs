using Corssbones.ALPR.Business;
using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;

namespace Crossbones.ALPR.Business.NumberPlates.View
{
    public class GetNumberPlate : SysSerialItemMessage
    {
        public GetNumberPlate(SysSerial sysSerial, GetQueryFilter filter) : base(sysSerial)
        {
            Filter = filter;
        }

        public Pager Paging { get; set; }
        public GetQueryFilter Filter { get; set; }
    }
}
