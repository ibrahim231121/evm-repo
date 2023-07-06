using Crossbones.ALPR.Common;
using Crossbones.ALPR.Common.ValueObjects;
using DTO = Crossbones.ALPR.Models.DTOs;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crossbones.ALPR.Api.ExportDetails
{
    [Route("ExportDetail")]
    public class ExportDetailController : BaseController
    {
        readonly IExportDetailService exportDetailService;
        ValidateModel<DTO.ExportDetailDTO> validateModel;

        public ExportDetailController(ApiParams feature, IExportDetailService _exportDetailService) : base(feature)
        {
            exportDetailService = _exportDetailService;
            validateModel = new ValidateModel<DTO.ExportDetailDTO>();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] DTO.ExportDetailDTO exportDetail)
        {
            (bool isValid, string errorList) = validateModel.Validate(exportDetail);
            if (isValid)
            {
                var Id = await exportDetailService.Add(exportDetail);
                return Created($"{baseUrl}/ExportDetail/{Id}", Id);
            }
            else
            {
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = errorList });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pager paging)
        {
            return PagedResult(await exportDetailService.GetAll(paging));
        }

        [HttpGet("{recId}")]
        public async Task<IActionResult> GetOne(long recId)
        {
            var res = await exportDetailService.Get(new RecId(recId));
            return Ok(res);
        }

        [HttpPut("{recId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change(long recId, [FromBody] DTO.ExportDetailDTO exportDetail)
        {
            (bool isValid, string errorList) = validateModel.Validate(exportDetail);
            if (isValid)
            {
                await exportDetailService.Change(new RecId(recId), exportDetail);
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
            await exportDetailService.Delete(new RecId(recId));
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteAll()
        {
            await exportDetailService.DeleteAll();
            return NoContent();
        }

        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok(true);
        }
    }
}