using Crossbones.ALPR.Api.NumberPlates.Service;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Api.NumberPlates
{
    [Route("LicensePlate")]
    public class NumberPlateController : BaseController
    {
        readonly INumberPlateService _service;
        public NumberPlateController(ApiParams feature, INumberPlateService service) : base(feature) => _service = service;

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] DTO.NumberPlateDTO numberPlates)
        {
            var RecId = await _service.Add(numberPlates);
            return Ok(new { statusCode = StatusCodes.Status201Created, message = $"Record against {RecId} successfully added" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pager paging)
        {
            return PaginatedOk(await _service.GetAll(paging));
        }

        [HttpGet("{numberPlateId}/history")]
        public async Task<IActionResult> GetNumberPlateHistory(long numberPlateId, [FromQuery] Pager paging)
        {
            RecId id = new RecId(numberPlateId);
            return PaginatedOk(await _service.GetNumberPlateHistory(id, paging));
        }

        [HttpGet("HotList/{hotListId}")]
        public async Task<IActionResult> GetAllByHotListId([FromQuery] Pager paging, long hotListId)
        {
            var numberPlates = await _service.GetAllByHotList(paging, hotListId);
            return PagedResult(numberPlates);
        }

        [HttpGet("{sysSerial}")]
        public async Task<IActionResult> GetOne(long sysSerial)
        {
            var res = await _service.Get(new RecId(sysSerial));
            return Ok(res);
        }

        [HttpPut("{RecId}")]
        //[ProducesResponseType(204)]
        public async Task<IActionResult> Change(long sysSerial, [FromBody] DTO.NumberPlateDTO numberPlates)
        {
            await _service.Change(new RecId(sysSerial), numberPlates);
            return Ok(new { statusCode = StatusCodes.Status204NoContent, message = "Successfully updated" });
        }

        [HttpDelete("{RecId}")]
        //[ProducesResponseType(204)]
        public async Task<IActionResult> DeleteOne(long RecId)
        {
            await _service.Delete(new RecId(RecId));
            return Ok(new { statusCode = StatusCodes.Status200OK, message = "Successfully deleted" });
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