using Crossbones.ALPR.Common;
using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;
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
        ValidateModel<DTO.HotListNumberPlateDTO> validateModel;

        public HotListNumberPlateController(ApiParams feature, IHotListNumberPlateService _hotListNumberPlateService) : base(feature)
        {
            hotListNumberPlateService = _hotListNumberPlateService;
            validateModel = new ValidateModel<DTO.HotListNumberPlateDTO>();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] DTO.HotListNumberPlateDTO hotListNumberPlate)
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

        [HttpGet("{recId}")]
        public async Task<IActionResult> GetOne(long recId)
        {
            var res = await hotListNumberPlateService.Get(new RecId(recId));
            return Ok(res);
        }

        [HttpPut("{recId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change(long recId, [FromBody] DTO.HotListNumberPlateDTO hotListNumberPlate)
        {
            (bool isValid, string errorList) = validateModel.Validate(hotListNumberPlate);
            if (isValid)
            {
                await hotListNumberPlateService.Change(new RecId(recId), hotListNumberPlate);
                return NoContent();
            }
            else
            {
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = errorList });
            }
        }

        [HttpDelete("{recId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteOne(long recId)
        {
            await hotListNumberPlateService.Delete(new RecId(recId));
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