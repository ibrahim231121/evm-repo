using Crossbones.ALPR.Api.HotListDataSourceMapping.Service;
using Crossbones.Modules.Api;
using Microsoft.AspNetCore.Mvc;

namespace Crossbones.ALPR.Api.HotListDataSourceMapping
{
    [Route("DataSourceMapping")]
    public class HotListDataSourceMappingController : BaseController
    {
        private readonly IHotListDataSourceMappingService _service;

        public HotListDataSourceMappingController(ApiParams feature, IHotListDataSourceMappingService service) : base(feature) => _service = service;

        [HttpPost("recId")]
        [Route("ExecuteMapping")]
        public async Task<IActionResult> ExecuteMapping([FromQuery] long recId)
        {
            var response = await _service.ExecuteMappingForSingleDataSource(recId);
            return Ok(response);
        }

        [HttpPost]
        [Route("ExecuteMappingForAll")]
        public async Task<IActionResult> ExecuteMappingForAll()
        {
            var response = await _service.ExecuteMappingForAllDataSources();
            return Ok(response);
        }

        [HttpPost("recId")]
        [Route("EnqueJobSingleDataSource")]
        public async Task<IActionResult> EnqueJobSingleDataSource([FromQuery] long recId)
        {
            var jobId = await _service.EnqueJobSingleDataSource(recId);
            string response = jobId.Equals(string.Empty) ? "Job initiated error" : "Job started successfully";
            return Ok(response);
        }

        [HttpPost]
        [Route("EnqueJobAllDataSources")]
        public async Task<IActionResult> EnqueJobAllDataSources()
        {
            await _service.EnqueJobAllDataSources();
            
            return Ok("Job enqueued successfully");
        }

        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok(true);
        }
    }
}