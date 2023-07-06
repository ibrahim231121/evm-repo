using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corssbones.ALPR.Business.NumberPlateHistory.Get
{
    public class GetNumberPlateHistoryItem : RecIdItemMessage
    {
        public GetNumberPlateHistoryItem(RecId recId, Pager pager, GridFilter filter, GridSort sort):base(recId)
        {
            Pager = pager;
            Filter = filter;
            Sort = sort;
        }

        public Pager Pager { get; }
        public GridFilter Filter { get; }
        public GridSort Sort { get; }
    }
}
