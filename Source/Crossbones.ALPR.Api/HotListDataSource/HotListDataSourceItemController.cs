using Crossbones.Modules.Api;
using Microsoft.AspNetCore.Mvc;
using Crossbones.ALPR.Common.ValueObjects;
using Crossbones.Modules.Common.Pagination;
using Crossbones.ALPR.Models.Items;
using Crossbones.ALPR.Api.HotListDataSource.Service;
using Corssbones.ALPR.Database.Entities;

namespace Crossbones.ALPR.Api.HotListDataSource
{
    [Route("HotListDataSource")]
    public class HotListDataSourceItemController : BaseController
    {
        readonly IHotListDataSourceItemService _service;

        public HotListDataSourceItemController(ApiParams feature, IHotListDataSourceItemService service) : base(feature) => _service = service;
        
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Add([FromBody] HotlistDataSource hotListDataSourceItem)
        {
            var SysSerial = await _service.Add(hotListDataSourceItem);

            return Created($"{baseUrl}/HotListDataSource/{SysSerial}", SysSerial);
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
        public async Task<IActionResult> Change(long SysSerial, [FromBody] HotlistDataSource hotListDataSourceItem)
        {
            await _service.Change(new SysSerial(SysSerial), hotListDataSourceItem);
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
