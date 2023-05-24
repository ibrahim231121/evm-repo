using M = Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Api.NumberPlates.Service;

namespace Crossbones.ALPR.Api.NumberPlates
{
    [Route("LicensePlate")]
    public class NumberPlateController : BaseController
    {
        readonly INumberPlateService _service;
        public NumberPlateController(ApiParams feature, INumberPlateService service) : base(feature) => _service = service;

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] M.NumberPlates numberPlates)
        {
            var SysSerial = await _service.Add(numberPlates);
            return Created($"{baseUrl}/LicensePlate/{SysSerial}", SysSerial);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pager paging)
        {
            return PagedResult(await _service.GetAll(paging));
        }

        [HttpGet("{SysSerial}")]
        public async Task<IActionResult> GetOne(long sysSerial)
        {
            var res = await _service.Get(new SysSerial(sysSerial));
            return Ok(res);
        }
        [HttpPut("{SysSerial}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change(long sysSerial, [FromBody] M.NumberPlates numberPlates)
        {
            await _service.Change(new SysSerial(sysSerial), numberPlates);
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
