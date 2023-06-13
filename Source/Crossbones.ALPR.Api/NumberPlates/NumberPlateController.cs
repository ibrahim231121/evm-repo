using Crossbones.ALPR.Api.NumberPlates.Service;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using M = Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Api.NumberPlates
{
    [Route("LicensePlate")]
    public class NumberPlateController : BaseController
    {
        readonly INumberPlateService _service;
        public NumberPlateController(ApiParams feature, INumberPlateService service) : base(feature) => _service = service;

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] M.NumberPlateItem numberPlates)
        {
            var SysSerial = await _service.Add(numberPlates);
            return Ok(new { statusCode = StatusCodes.Status201Created, message = $"Record against {SysSerial} successfully added" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pager paging)
        {
            return PaginatedOk(await _service.GetAll(paging));
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
            var res = await _service.Get(new SysSerial(sysSerial));
            return Ok(res);
        }

        [HttpPut("{SysSerial}")]
        //[ProducesResponseType(204)]
        public async Task<IActionResult> Change(long sysSerial, [FromBody] M.NumberPlateItem numberPlates)
        {
            await _service.Change(new SysSerial(sysSerial), numberPlates);
            return Ok(new { statusCode = StatusCodes.Status204NoContent, message = "Successfully updated" });
        }

        [HttpDelete("{SysSerial}")]
        //[ProducesResponseType(204)]
        public async Task<IActionResult> DeleteOne(long SysSerial)
        {
            try
            {
                await _service.Delete(new SysSerial(SysSerial));
                return Ok(new { statusCode = StatusCodes.Status200OK, message = "Successfully deleted" });
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FK_HotListNumberPlates_NumberPlates") && ex.Message.Contains("conflicted"))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Can not delete data since it is associated with HotList");
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{ex.Message}");
                }
            }

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