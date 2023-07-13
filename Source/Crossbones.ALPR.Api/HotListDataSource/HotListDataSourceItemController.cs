using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Api.HotListDataSource.Service;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Microsoft.AspNetCore.Mvc;
using DTO = Crossbones.ALPR.Models.DTOs;

namespace Crossbones.ALPR.Api.HotListDataSource
{
    [Route("HotListDataSource")]
    public class HotListDataSourceItemController : BaseController
    {
        readonly IHotListDataSourceItemService _service;

        public HotListDataSourceItemController(ApiParams feature, IHotListDataSourceItemService service) : base(feature) => _service = service;

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] DTO.HotListDataSourceDTO hotListDataSourceItem)
        {
            var RecId = await _service.Add(hotListDataSourceItem);

            return Created($"{baseUrl}/HotListDataSource/{RecId}", RecId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pager paging)
        {
            return PaginatedOk(await _service.GetAll(paging));
        }

        [HttpGet("{RecId}")]
        public async Task<IActionResult> GetOne(long RecId)
        {
            var res = await _service.Get(new RecId(RecId));
            return Ok(res);
        }

        [HttpPut("{RecId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change(long RecId, [FromBody] HotlistDataSource hotListDataSourceItem)
        {
            await _service.Change(new RecId(RecId), hotListDataSourceItem);
            return NoContent();
        }

        [HttpDelete("{RecId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteOne(long RecId)
        {
            await _service.Delete(new RecId(RecId));
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
