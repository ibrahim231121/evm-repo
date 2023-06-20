using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Crossbones.ALPR.Api.CapturedPlate
{
    [Route("CapturedPlate")]
    public class CapturedPlateController : BaseController
    {
        readonly ICapturedPlateService _service;

        public CapturedPlateController(ApiParams feature, ICapturedPlateService service) : base(feature) => _service = service;

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] CapturedPlateDTO capturedPlateItem)
        {
            var RecId = await _service.Add(capturedPlateItem);

            return Created($"{baseUrl}/CapturedPlate/{RecId}", RecId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] long userId, [FromQuery] Pager paging, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            GridFilter filter;
            GridSort sort;
            List<long> hotListIds = null;

            Microsoft.Extensions.Primitives.StringValues HeaderGridFilter;
            Microsoft.Extensions.Primitives.StringValues HeaderGridSort;
            Microsoft.Extensions.Primitives.StringValues HeaderHotListIds;

            try
            {
                HttpContext.Request.Headers.TryGetValue("GridFilter", out HeaderGridFilter);
                filter = JsonConvert.DeserializeObject<GridFilter>(HeaderGridFilter);

                HttpContext.Request.Headers.TryGetValue("GridSort", out HeaderGridSort);
                sort = JsonConvert.DeserializeObject<GridSort>(HeaderGridSort);

                HttpContext.Request.Headers.TryGetValue("HotListIds", out HeaderHotListIds);
                hotListIds = JsonConvert.DeserializeObject<List<long>>(HeaderHotListIds);
            }
            catch
            {
                filter = new GridFilter();
                filter.Logic = "and";
                filter.Filters = new List<GridFilter>();

                sort = new GridSort();
            }

            var res = await _service.GetAll(userId, startDate, endDate, paging, filter, sort, hotListIds);

            return PaginatedOk(res);
        }

        [HttpGet("{RecId}")]
        public async Task<IActionResult> GetOne(long RecId)
        {
            var res = await _service.Get(new RecId(RecId));
            return Ok(res);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change([FromQuery] long RecId, [FromBody] CapturedPlateDTO capturedPlateItem)
        {
            await _service.Change(new RecId(RecId), capturedPlateItem);
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
        public async Task<IActionResult> DeleteAll([FromQuery] long userId)
        {
            await _service.DeleteAll(userId);
            return NoContent();
        }

        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok(true);
        }
    }
}
