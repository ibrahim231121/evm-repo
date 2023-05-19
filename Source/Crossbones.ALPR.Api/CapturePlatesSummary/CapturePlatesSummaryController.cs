using Crossbones.ALPR.Api.CapturedPlate;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Crossbones.ALPR.Common.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crossbones.ALPR.Api.CapturePlatesSummary
{
    [Route("CapturePlatesSummary")]
    public class CapturePlatesSummaryController: BaseController
    {
        ICapturePlatesSummaryService _service;

        public CapturePlatesSummaryController(ApiParams feature, ICapturePlatesSummaryService service) : base(feature) => _service = service;

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] CapturePlatesSummaryItem capturedPlateSummaryItem)
        {
            var SysSerial = await _service.Add(capturedPlateSummaryItem);

            return Created($"{baseUrl}/CapturePlatesSummary/{SysSerial}", SysSerial);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAll([FromQuery] Pager paging, long userId = 0)
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

            var res = await _service.GetAll(paging, filter, sort, userId);

            return PagedResult(res);
        }

        [HttpGet("{userId}/{capturedPlateId}")]
        public async Task<IActionResult> GetOne(long userId, long capturedPlateId)
        {
            var res = await _service.Get(userId, capturedPlateId);
            return Ok(res);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change([FromBody] CapturePlatesSummaryItem capturedPlateSummaryItem)
        {
            await _service.Change(capturedPlateSummaryItem);
            return NoContent();
        }

        [HttpDelete("{userId}/{capturedPlateId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteOne(long userId, long capturedPlateId)
        {
            await _service.Delete(userId, capturedPlateId);
            return NoContent();
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteAll(long userId)
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
