using Corssbones.ALPR.Business.Enums;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;

namespace Corssbones.ALPR.Business.HotListNumberPlates.Get
{
    public class GetHotListNumberPlate : RecIdItemMessage
    {
        public Pager Paging { get; set; }
        public GetQueryFilter Filter { get; set; }
        public GetHotListNumberPlate(RecId recId, GetQueryFilter filter) : base(recId)
        {
            Filter = filter;
        }
    }
}