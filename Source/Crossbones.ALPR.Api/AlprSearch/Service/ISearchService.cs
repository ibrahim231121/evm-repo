using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.DTOs;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;

namespace Crossbones.ALPR.Api.AlprSearch.Service
{
    public interface ISearchService
    {
        Task<List<string>> SearchNumberPlate(string plateNumber);
        Task<PageResponse<AlprSearchResponse>> SearchNumberPlate(AlprSearchParams alprSearchParams,Pager pager, GridFilter gridFilter, GridSort sort);
    }
}
