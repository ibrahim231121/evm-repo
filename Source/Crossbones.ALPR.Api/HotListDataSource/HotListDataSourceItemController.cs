using Entities = Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Api.HotListDataSource.Service;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using DTO = Crossbones.ALPR.Models.DTOs;
using Crossbones.ALPR.Common;
using Corssbones.ALPR.Database.Entities;
using Microsoft.AspNetCore.Http;
using Crossbones.ALPR.Api.HotListNumberPlates;

namespace Crossbones.ALPR.Api.HotListDataSource
{
    [Route("HotListDataSource")]
    public class HotListDataSourceItemController : BaseController
    {
        readonly IHotListDataSourceItemService _service;
        ValidateModel<DTO.HotListDataSourceDTO> validateModel;

        public HotListDataSourceItemController(ApiParams feature, IHotListDataSourceItemService service) : base(feature)
        {
            _service = service;
            validateModel = new ValidateModel<DTO.HotListDataSourceDTO>();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] DTO.HotListDataSourceDTO hotListDataSourceItem)
        {
            (bool isValid, string errorList) = validateModel.Validate(hotListDataSourceItem);
            if (isValid)
            {
                var recId = await _service.Add(hotListDataSourceItem);

                return Created($"{baseUrl}/HotListDataSource/{recId}", recId);
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

        [HttpGet("{recId}")]
        public async Task<IActionResult> GetOne(long recId)
        {
            var res = await _service.Get(new RecId(recId));
            return Ok(res);
        }

        [HttpPut("{recId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change(long recId, [FromBody] Entities.HotlistDataSource hotListDataSourceItem)
        {
            //(bool isValid, string errorList) = validateModel.Validate(hotListNumberPlate);
            //if (isValid)
            //{
            //    await _service.Change(new RecId(recId), hotListDataSourceItem);
            //    return NoContent();
            //}
            //else
            //{
            //    return BadRequest(new { statusCode = StatusCodes.Status400BadRequest, message = errorList });
            //}
            await _service.Change(new RecId(recId), hotListDataSourceItem);
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
