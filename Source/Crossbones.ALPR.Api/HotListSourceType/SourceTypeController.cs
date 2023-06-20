
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
            var RecId = await _service.Add(sourceTypeItem);

            return Created($"{baseUrl}/SourceType/{RecId}", RecId);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pager paging)
        {



            return PagedResult(await _service.GetAll(paging));
        }

        [HttpGet("{RecId}")]
        public async Task<IActionResult> GetOne(long RecId)
        {
            var res = await _service.Get(new RecId(RecId));
            return Ok(res);
        }

        [HttpPut("{RecId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change(long RecId, [FromBody] SourceType sourceTypeItem)
        {
            await _service.Change(new RecId(RecId), sourceTypeItem);
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
