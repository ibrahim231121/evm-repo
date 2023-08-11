using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.DTOs;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;

namespace Crossbones.ALPR.Api.AlprSearch.Service
{
    public interface ISearchService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="plateNumber">Number plate string to search among records</param>
        /// <returns>List of number plates which match with provided string</returns>
        Task<List<string>> SearchNumberPlate(string plateNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alprSearchParams">Required while searching for records with provided filters from UI</param>
        /// <param name="pager"></param>
        /// <param name="gridFilter"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        Task<PageResponse<AlprSearchResponse>> SearchNumberPlate(AlprSearchParams alprSearchParams,Pager pager, GridFilter gridFilter, GridSort sort);
    }
}