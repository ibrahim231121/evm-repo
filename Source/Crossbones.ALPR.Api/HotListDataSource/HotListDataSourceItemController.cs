using Entities = Corssbones.ALPR.Database.Entities;
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
            var recId = await _service.Add(hotListDataSourceItem);

            return Created($"{baseUrl}/HotListDataSource/{recId}", recId);
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
