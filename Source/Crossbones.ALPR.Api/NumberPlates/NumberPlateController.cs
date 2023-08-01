using Crossbones.ALPR.Api.NumberPlates.Service;
using Crossbones.ALPR.Common;
using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Crossbones.ALPR.Api.NumberPlates
{
    [Route("LicensePlate")]
    public class NumberPlateController : BaseController
    {
        readonly INumberPlateService _service;
        ValidateModel<DTO.NumberPlateDTO> validateModel;
        public NumberPlateController(ApiParams feature, INumberPlateService service) : base(feature) 
        { 
            _service = service;
            validateModel = new ValidateModel<DTO.NumberPlateDTO>();
        }

        [HttpPost]
        //[ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] DTO.NumberPlateDTO numberPlates)
        {
            (bool isValid, string errorList) = validateModel.Validate(numberPlates);

            if (isValid)
            {
                var recId = await _service.Add(numberPlates);
                return Ok(new { statusCode = StatusCodes.Status201Created, message = $"Record against {recId} successfully added" });
            }
            else
            {
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = errorList });
            }            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pager paging)
        {
            return PaginatedOk(await _service.GetAll(paging));
        }

        [HttpGet("{numberPlateId}/history")]
        public async Task<IActionResult> GetNumberPlateHistory(long numberPlateId, [FromQuery] Pager paging)
        {
            RecId recId = new RecId(numberPlateId);
            return PaginatedOk(await _service.GetNumberPlateHistory(recId, paging));
        }

        [HttpGet("HotList/{hotListId}")]
        public async Task<IActionResult> GetAllByHotListId([FromQuery] Pager paging, long hotListId)
        {
            var numberPlates = await _service.GetAllByHotList(paging, hotListId);
            return PagedResult(numberPlates);
        }

        [HttpGet("{recId}")]
        public async Task<IActionResult> GetOne(long recId)
        {
            var res = await _service.Get(new RecId(recId));
            return Ok(res);
        }

        [HttpPut("{recId}")]
        //[ProducesResponseType(204)]
        public async Task<IActionResult> Change(long recId, [FromBody] DTO.NumberPlateDTO numberPlates)
        {
            (bool isValid, string errorList) = validateModel.Validate(numberPlates);
            
            if (isValid)
            { 
                await _service.Change(new RecId(recId), numberPlates);
                return Ok(new { statusCode = StatusCodes.Status204NoContent, message = "Successfully updated" });
            }
            else
            {
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = errorList });
            }
        }

        [HttpDelete("{recId}")]
        //[ProducesResponseType(204)]
        public async Task<IActionResult> DeleteOne(long recId)
        {
            await _service.Delete(new RecId(recId));
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