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
            var recId = await _service.Add(numberPlatesTemp);
            return Created($"{baseUrl}/LicensePlatesTemp/{recId}", recId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pager paging)
        {
            return PagedResult(await _service.GetAll(paging));
        }

        [HttpGet("{recId}")]
        public async Task<IActionResult> GetOne(long recId)
        {
            var res = await _service.Get(new RecId(recId));
            return Ok(res);
        }

        [HttpPut("{recId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change(long recId, [FromBody] DTO.NumberPlateTempDTO numberPlatesTemp)
        {
            await _service.Change(new RecId(recId), numberPlatesTemp);
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