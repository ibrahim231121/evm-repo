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

namespace Crossbones.ALPR.Api.CapturePlatesSummaryStatus
{
    [Route("CapturePlatesSummaryStatus")]
    public class CapturePlatesSummaryStatusController: BaseController
    {
        ICapturePlatesSummaryStatusService _service;

        public CapturePlatesSummaryStatusController(ApiParams feature, ICapturePlatesSummaryStatusService service) : base(feature) => _service = service;

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] CapturePlatesSummaryStatusItem capturedPlateSummaryStatusItem)
        {
            var SysSerial = await _service.Add(capturedPlateSummaryStatusItem);

            return Created($"{baseUrl}/CapturedPlate/{SysSerial}", SysSerial);
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

            var res = await _service.GetAll(paging, filter, sort);

            return PagedResult(res);
        }

        [HttpGet("{SysSerial}")]
        public async Task<IActionResult> GetOne(long SysSerial)
        {
            var res = await _service.Get(new SysSerial(SysSerial));
            return Ok(res);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change([FromQuery] long SysSerial, [FromBody] CapturePlatesSummaryStatusItem capturedPlateSummaryStatusItem)
        {
            await _service.Change(new SysSerial(SysSerial), capturedPlateSummaryStatusItem);
            return NoContent();
        }

        [HttpDelete("{SysSerial}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteOne(long SysSerial)
        {
            await _service.Delete(new SysSerial(SysSerial));
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
