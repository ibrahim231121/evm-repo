using Crossbones.ALPR.Api.AlprSearch.Service;
using Crossbones.ALPR.Models;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Crossbones.ALPR.Api.AlprSearch
{
    [Route("ALPR")]
    public class SearchController : BaseController
    {
        ISearchService service;
        public SearchController(ApiParams feature, ISearchService _service) : base(feature)
        {
            service = _service;
        }

        [HttpPost("Search")]
        public async Task<IActionResult> ALPRSearchByFilters([FromBody] AlprSearchParams alprSearchParams = null)
        {
            GridFilter filter;
            GridSort sort;
            Pager paging;

            Microsoft.Extensions.Primitives.StringValues HeaderGridFilter;
            Microsoft.Extensions.Primitives.StringValues HeaderGridSort;
            Microsoft.Extensions.Primitives.StringValues HeaderPager;

            try
            {
                HttpContext.Request.Headers.TryGetValue("GridFilter", out HeaderGridFilter);
                filter = JsonConvert.DeserializeObject<GridFilter>(HeaderGridFilter);

                HttpContext.Request.Headers.TryGetValue("GridSort", out HeaderGridSort);
                sort = JsonConvert.DeserializeObject<GridSort>(HeaderGridSort);

                HttpContext.Request.Headers.TryGetValue("Paging", out HeaderPager);
                paging = JsonConvert.DeserializeObject<Pager>(HeaderPager);
            }
            catch
            {
                filter = new GridFilter();
                filter.Logic = "and";
                filter.Filters = new List<GridFilter>();

                sort = new GridSort() { Field = "numberPlate", Dir = "asc" };

                paging = new Pager() { Page = 1, Size = 1000 };
            }
            return Ok(await service.SearchNumberPlate(alprSearchParams,paging, filter, sort));
        }

        [HttpGet("Search/NumberPlate/{numberPlate}")]
        public async Task<IActionResult> ALPRSearchByNumberPlate(string numberPlate)
        {
            if (!string.IsNullOrEmpty(numberPlate))
            {
                return Ok(await service.SearchNumberPlate(numberPlate));
            }
            else
            {
                return BadRequest("Invalid number plate value");
            }
        }
    }
}