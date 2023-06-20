using Crossbones.ALPR.Api.HotList.Service;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Crossbones.ALPR.Api.HotList
{
    [Route("HotList")]
    public class HotListItemController : BaseController
    {
        readonly IHotListItemService _service;

        public HotListItemController(ApiParams feature, IHotListItemService service) : base(feature) => _service = service;

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] HotListDTO hotListItem)
        {
            var RecId = await _service.Add(hotListItem);

            return Created($"{baseUrl}/HotList/{RecId}", RecId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pager paging)
        {
            GridFilter filter;
            GridSort sort;

            Microsoft.Extensions.Primitives.StringValues HeaderGridFilter;
            Microsoft.Extensions.Primitives.StringValues HeaderGridSort;

            try
            {
                HttpContext.Request.Headers.TryGetValue("GridFilter", out HeaderGridFilter);
                filter = JsonConvert.DeserializeObject<GridFilter>(HeaderGridFilter);

                HttpContext.Request.Headers.TryGetValue("GridSort", out HeaderGridSort);
                sort = JsonConvert.DeserializeObject<GridSort>(HeaderGridSort);
            }
            catch
            {
                filter = new GridFilter();
                filter.Logic = "and";
                filter.Filters = new List<GridFilter>();

                sort = new GridSort();
            }

            return PaginatedOk(await _service.GetAll(paging, filter, sort));
        }

        [HttpGet("{RecId}")]
        public async Task<IActionResult> GetOne(long RecId)
        {
            var res = await _service.Get(new RecId(RecId));
            return Ok(res);
        }

        [HttpPut("{RecId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change(long RecId, [FromBody] HotListDTO hotListItem)
        {
            await _service.Change(new RecId(RecId), hotListItem);
            return NoContent();
        }

        [HttpDelete("{RecId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteOne(long RecId)
        {
            await _service.Delete(new RecId(RecId));
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteAll()
        {
            await _service.DeleteAll();
            return NoContent();
        }

        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok(true);
        }
    }
}
