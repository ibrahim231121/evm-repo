using Crossbones.ALPR.Api.NumberPlates.Service;
using Crossbones.ALPR.Api.NumberPlatesTemp.Service;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using M = Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Api.NumberPlatesTemp
{
    [Route("LicensePlatesTemp")]
    public class NumberPlatesTempController : BaseController
    {
        readonly INumberPlatesTempService _service;
        public NumberPlatesTempController(ApiParams feature, INumberPlatesTempService service) : base(feature)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] M.NumberPlatesTemp numberPlatesTemp)
        {
            var SysSerial = await _service.Add(numberPlatesTemp);
            return Created($"{baseUrl}/LicensePlatesTemp/{SysSerial}", SysSerial);
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
        public async Task<IActionResult> Change(long sysSerial, [FromBody] M.NumberPlatesTemp numberPlatesTemp)
        {
            await _service.Change(new SysSerial(sysSerial), numberPlatesTemp);
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
