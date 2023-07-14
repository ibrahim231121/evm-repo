using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;

namespace Corssbones.ALPR.Business.ExportDetail.Get
{
    public class GetExportDetail : RecIdItemMessage
    {
        public Pager Paging { get; set; }
        public GetQueryFilter Filter { get; set; }
        public GetExportDetail(RecId recId, GetQueryFilter filter) : base(recId)
        {
            Filter = filter;
        }
    }
}
