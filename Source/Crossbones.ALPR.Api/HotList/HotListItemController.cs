using Crossbones.ALPR.Api.HotList.Service;
using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;
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
        public async Task<IActionResult> Add([FromBody] DTO.HotListDTO hotListItem)
        {
            var recId = await _service.Add(hotListItem);

            return Created($"{baseUrl}/HotList/{recId}", recId);
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

        [HttpGet("{recId}")]
        public async Task<IActionResult> GetOne(long recId)
        {
            var res = await _service.Get(new RecId(recId));
            return Ok(res);
        }

        [HttpPut("{recId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change(long recId, [FromBody] DTO.HotListDTO hotListItem)
        {
            await _service.Change(new RecId(recId), hotListItem);
            return NoContent();
        }

        [HttpDelete("{recId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteOne(long recId)
        {
            await _service.Delete(new RecId(recId));
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteMany([FromQuery]string hotlistIds)
        {
            List<long> idsToDelete = new List<long>();

            if (!string.IsNullOrEmpty(hotlistIds))
            {
                string[] ids = hotlistIds.Split(",");

                if(ids != null && ids.Length > 0)
                {
                    foreach(string id in ids)
                    {
                        long hotlistId;

                        bool parsed = long.TryParse(id, out hotlistId);

                        if (parsed)
                        {
                            idsToDelete.Add(hotlistId);
                        }
                        else
                        {
                            throw new InvalidCastException("Unable to cast paramter.");
                        }
                    }
                }
            }

            await _service.DeleteMany(idsToDelete);
            return NoContent();
        }

        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok(true);
        }
    }
}
