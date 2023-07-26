using Crossbones.ALPR.Common;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.ALPR.Models.CapturedPlate;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Common.Queryables;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Crossbones.ALPR.Api.CapturedPlate
{
    [Route("CapturedPlate")]
    public class CapturedPlateController : BaseController
    {
        readonly ICapturedPlateService _service;
        ValidateModel<CapturedPlateDTO> validateModel;

        public CapturedPlateController(ApiParams feature, ICapturedPlateService service) : base(feature)
        {
            _service = service;
            validateModel = new ValidateModel<CapturedPlateDTO>();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] CapturedPlateDTO capturedPlateItem)
        {
            (bool isValid, string errorList) = validateModel.Validate(capturedPlateItem);
            if (isValid)
            {
                var recId = await _service.Add(capturedPlateItem);

                return Created($"{baseUrl}/CapturedPlate/{recId}", recId);
            }
            else
            {
                return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = errorList });
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] long userId, long hotListId, [FromQuery] Pager paging, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            GridFilter filter;
            GridSort sort;

            Microsoft.Extensions.Primitives.StringValues HeaderGridFilter;
            Microsoft.Extensions.Primitives.StringValues HeaderGridSort;

            try
            {
                HttpContext.Request.Headers.TryGetValue("GridFilter", out HeaderGridFilter);
                filter = JsonConvert.DeserializeObject<GridFilter>(HeaderGridFilter);

                HttpContext.Request.Headers.TryGetValue("GridSort", out HeaderGridSort);
                sort = JsonConvert.DeserializeObject<GridSort>(HeaderGridSort);
            }
            catch
            {
                filter = new GridFilter();
                filter.Logic = "and";
                filter.Filters = new List<GridFilter>();

                sort = new GridSort();
            }

            var res = await _service.GetAll(userId, startDate, endDate, paging, filter, sort, hotListId);

            return PaginatedOk(res);
        }

        [HttpGet("{recId}")]
        public async Task<IActionResult> GetOne(long recId)
        {
            var res = await _service.Get(new RecId(recId));
            return Ok(res);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change([FromQuery] long recId, [FromBody] CapturedPlateDTO capturedPlateItem)
        {
            (bool isValid, string errorList) = validateModel.Validate(capturedPlateItem);
            if (isValid)
            {
                await _service.Change(new RecId(recId), capturedPlateItem);
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
            await _service.Delete(new RecId(recId));
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteAll([FromQuery] long userId)
        {
            await _service.DeleteAll(userId);
            return NoContent();
        }

        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok(true);
        }
    }
}