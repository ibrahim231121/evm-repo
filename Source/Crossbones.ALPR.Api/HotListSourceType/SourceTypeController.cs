
using Corssbones.ALPR.Database.Entities;
using Crossbones.ALPR.Api.HotListSourceType.Service;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Api;
using Crossbones.Modules.Common.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace Crossbones.ALPR.Api.HotListSourceType
{
    [Route("HotListSourceType")]
    public class SourceTypeController : BaseController
    {
        readonly ISourceTypeService _service;

        public SourceTypeController(ApiParams feature, ISourceTypeService service) : base(feature) => _service = service;

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] SourceType sourceTypeItem)
        {
            var recId = await _service.Add(sourceTypeItem);

            return Created($"{baseUrl}/SourceType/{recId}", recId);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pager paging)
        {



            return PagedResult(await _service.GetAll(paging));
        }

        [HttpGet("{recId}")]
        public async Task<IActionResult> GetOne(long recId)
        {
            var res = await _service.Get(new RecId(recId));
            return Ok(res);
        }

        [HttpPut("{recId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change(long recId, [FromBody] SourceType sourceTypeItem)
        {
            await _service.Change(new RecId(recId), sourceTypeItem);
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
