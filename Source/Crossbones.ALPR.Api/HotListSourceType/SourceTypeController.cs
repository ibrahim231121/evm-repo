
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
            var SysSerial = await _service.Add(sourceTypeItem);

            return Created($"{baseUrl}/SourceType/{SysSerial}", SysSerial);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Pager paging)
        {



            return PagedResult(await _service.GetAll(paging));
        }

        [HttpGet("{SysSerial}")]
        public async Task<IActionResult> GetOne(long SysSerial)
        {
            var res = await _service.Get(new SysSerial(SysSerial));
            return Ok(res);
        }

        [HttpPut("{SysSerial}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Change(long SysSerial, [FromBody] SourceType sourceTypeItem)
        {
            await _service.Change(new SysSerial(SysSerial), sourceTypeItem);
            return NoContent();
        }

        [HttpDelete("{SysSerial}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteOne(long SysSerial)
        {
            await _service.Delete(new SysSerial(SysSerial));
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
