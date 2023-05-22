using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;

namespace Corssbones.ALPR.Business.HotList.Get
{
    public class GetHotListNumberPlate : HotListItemMessage
    {
        public Pager Paging { get; set; }
        public GetQueryFilter Filter { get; set; }
        public GetHotListNumberPlate(SysSerial sysSerial, GetQueryFilter filter) : base(sysSerial)
        {
            Filter = filter;
        }
    }
}