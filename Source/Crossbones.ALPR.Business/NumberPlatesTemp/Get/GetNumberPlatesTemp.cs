using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;

namespace Corssbones.ALPR.Business.NumberPlatesTemp.Get
{
    public class GetNumberPlatesTemp : RecIdItemMessage
    {
        public GetNumberPlatesTemp(RecId sysSerial, GetQueryFilter filter) : base(sysSerial)
        {
            Filter = filter;
        }
        public Pager Paging { get; set; }
        public GetQueryFilter Filter { get; set; }
    }
}