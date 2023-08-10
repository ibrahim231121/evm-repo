using Corssbones.ALPR.Business.Search;
using Crossbones.ALPR.Api.NumberPlates.Service;
using Crossbones.ALPR.Models;
using Crossbones.ALPR.Models.DTOs;
using Crossbones.Modules.Common;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;

namespace Crossbones.ALPR.Api.AlprSearch.Service
{
    public class SearchService : ServiceBase, ISearchService
    {
        INumberPlateService numberPlateService;
        public SearchService(ServiceArguments args, INumberPlateService _numberPlateService) : base(args)
        {
            numberPlateService = _numberPlateService;
        }

        public async Task<List<string>> SearchNumberPlate(string plateNumber)
        {
            var list = await numberPlateService.GetNumberPlate(plateNumber);
            return list;            
        }       

        public async Task<PageResponse<AlprSearchResponse>> SearchNumberPlate(AlprSearchParams alprSearchParams,Pager pager, GridFilter gridFilter, GridSort sort)
        {
            if (alprSearchParams != null)
            {
                var query = new AlprAdvanceSearch(alprSearchParams, pager, gridFilter, sort);
                var res = await Inquire<PageResponse<AlprSearchResponse>>(query);

                return res;
            }
            else
            {
                var query = new AlprAdvanceSearch(pager, gridFilter, sort);
                var res = await Inquire<PageResponse<AlprSearchResponse>>(query);

                return res;
            }
           
        }
    }
}