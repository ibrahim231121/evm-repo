using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;

namespace Corssbones.ALPR.Business.SourceType.View
{
    public class GetSourceType : RecIdItemMessage
    {
        public GetSourceType(RecId recId, GetQueryFilter filter) : base(recId)
        {
            Filter = filter;
        }
        public Pager Paging { get; set; }
        public GetQueryFilter Filter { get; set; }

    }
}
