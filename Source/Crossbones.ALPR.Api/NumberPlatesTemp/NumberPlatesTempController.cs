using Crossbones.ALPR.Api.NumberPlatesTemp.Service;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using DTO = Crossbones.ALPR.Models.DTOs;

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
        public async Task<IActionResult> Add([FromBody] DTO.NumberPlateTempDTO numberPlatesTemp)
        {
            var RecId = await _service.Add(numberPlatesTemp);
            return Created($"{baseUrl}/LicensePlatesTemp/{RecId}", RecId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pager paging)
        {
            return PagedResult(await _service.GetAll(paging));
        }

        [HttpGet("{RecId}")]
        public async Task<IActionResult> GetOne(long sysSerial)
        {
            var res = await _service.Get(new RecId(sysSerial));
            return Ok(res);
        }

        [HttpPut("{RecId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change(long sysSerial, [FromBody] DTO.NumberPlateTempDTO numberPlatesTemp)
        {
            await _service.Change(new RecId(sysSerial), numberPlatesTemp);
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
