using Crossbones.ALPR.Common;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crossbones.ALPR.Api.HotListNumberPlates
{
    [Route("HotListNumberPlate")]
    public class HotListNumberPlateController : BaseController
    {
        readonly IHotListNumberPlateService hotListNumberPlateService;
        ValidateModel<HotListNumberPlateItem> validateModel;

        public HotListNumberPlateController(ApiParams feature, IHotListNumberPlateService _hotListNumberPlateService) : base(feature)
        {
            hotListNumberPlateService = _hotListNumberPlateService;
            validateModel = new ValidateModel<HotListNumberPlateItem>();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] HotListNumberPlateItem hotListNumberPlate)
        {
            (bool isValid, string errorList) = validateModel.Validate(hotListNumberPlate);
            if (isValid)
            {
                var Id = await hotListNumberPlateService.Add(hotListNumberPlate);
                return Created($"{baseUrl}/HotListNumberPlate/{Id}", Id);
            }
            else
            {
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = errorList });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pager paging)
        {
            var result = await hotListNumberPlateService.GetAll(paging);
            return PagedResult(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOne(long Id)
        {
            var res = await hotListNumberPlateService.Get(new SysSerial(Id));
            return Ok(res);
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change(long Id, [FromBody] HotListNumberPlateItem hotListNumberPlate)
        {
            (bool isValid, string errorList) = validateModel.Validate(hotListNumberPlate);
            if (isValid)
            {
                await hotListNumberPlateService.Change(new SysSerial(Id), hotListNumberPlate);
                return NoContent();
            }
            else
            {
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = errorList });
            }
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteOne(long Id)
        {
            await hotListNumberPlateService.Delete(new SysSerial(Id));
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteAll()
        {
            await hotListNumberPlateService.DeleteAll();
            return NoContent();
        }

        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok(true);
        }
    }
}