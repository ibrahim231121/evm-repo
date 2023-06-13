﻿using Crossbones.ALPR.Common.ValueObjects;
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
        public async Task<IActionResult> Add([FromBody] CapturedPlateItem capturedPlateItem)
        {
            var SysSerial = await _service.Add(capturedPlateItem);

            return Created($"{baseUrl}/CapturedPlate/{SysSerial}", SysSerial);
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

        [HttpGet("{SysSerial}")]
        public async Task<IActionResult> GetOne(long SysSerial)
        {
            var res = await _service.Get(new SysSerial(SysSerial));
            return Ok(res);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change([FromQuery] long SysSerial, [FromBody] CapturedPlateItem capturedPlateItem)
        {
            await _service.Change(new SysSerial(SysSerial), capturedPlateItem);
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
