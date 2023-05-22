using Crossbones.ALPR.Common;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.Items;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crossbones.ALPR.Api.ALPREvents.ExportDetails
{
    [Route("ExportDetail")]
    public class ExportDetailController : BaseController
    {
        readonly IExportDetailService exportDetailService;
        ValidateModel<ExportDetailItem> validateModel;

        public ExportDetailController(ApiParams feature, IExportDetailService _exportDetailService) : base(feature)
        {
            exportDetailService = _exportDetailService;
            validateModel = new ValidateModel<ExportDetailItem>();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] ExportDetailItem exportDetail)
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

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetOne(long Id)
        {
            var res = await exportDetailService.Get(new SysSerial(Id));
            return Ok(res);
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change(long Id, [FromBody] ExportDetailItem exportDetail)
        {
            (bool isValid, string errorList) = validateModel.Validate(exportDetail);
            if (isValid)
            {
                await exportDetailService.Change(new SysSerial(Id), exportDetail);
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
            await exportDetailService.Delete(new SysSerial(Id));
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