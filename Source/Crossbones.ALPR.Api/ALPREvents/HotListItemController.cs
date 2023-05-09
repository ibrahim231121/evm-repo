using Crossbones.Modules.Api;
using Microsoft.AspNetCore.Mvc;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using Crossbones.ALPR.Models.Items;

namespace Crossbones.ALPR.Api.ALPREvents
{
    [Route("HotList")]
    public class HotListItemController : BaseController
    {
        readonly IHotListItemService _service;

        public HotListItemController(ApiParams feature, IHotListItemService service) : base(feature) => _service = service;
        
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] HotListItem hotListItem)
        {
            var SysSerial = await _service.Add(hotListItem);

            return Created($"{baseUrl}/HotList/{SysSerial}", SysSerial);
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
        public async Task<IActionResult> Change(long SysSerial, [FromBody] HotListItem hotListItem)
        {
            await _service.Change(new SysSerial(SysSerial), hotListItem);
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
