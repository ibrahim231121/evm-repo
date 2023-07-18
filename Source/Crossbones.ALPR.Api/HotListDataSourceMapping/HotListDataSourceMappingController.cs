using Crossbones.ALPR.Api.HotListDataSourceMapping.Service;
using Crossbones.Modules.Api;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Crossbones.ALPR.Api.HotListDataSourceMapping
{
    [Route("DataSourceMapping")]
    public class HotListDataSourceMappingController : BaseController
    {
        private readonly IHotListDataSourceMappingService _service;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;

        public HotListDataSourceMappingController(ApiParams feature,
            IHotListDataSourceMappingService service
            //IBackgroundJobClient backgroundJobClient,
            //IRecurringJobManager recurringJobManager
            ) : base(feature)
        {
            _service = service;
            //_backgroundJobClient = backgroundJobClient;
            //_recurringJobManager = recurringJobManager;
        }

        [HttpGet]
        [Route("ExecuteMapping/{SysSerial}")]
        public async Task<IActionResult> ExecuteMapping(long SysSerial)
        {
            var response = await _service.ExecuteMappingForSingleDataSource(SysSerial);
            return Ok(response);
        }

        [HttpGet]
        [Route("ExecuteMappingForAll")]
        public async Task<IActionResult> ExecuteMappingForAll()
        {
            var response = await _service.ExecuteMappingForAllDataSources();
            return Ok(response);
        }

        [HttpGet]
        [Route("IFireAndForgetJob")]
        public string FireAndForgetJob()
        {
            //Fire - and - Forget Jobs
            //Fire - and - forget jobs are executed only once and almost immediately after creation.
            var jobId = _backgroundJobClient.Enqueue(() => _service.ExecuteMappingForAllDataSources());

            return $"Job ID: {jobId}. Mapping for all data sources has been executed once.";
        }

        [HttpGet]
        [Route("IDelayedJob")]
        public string DelayedJob()
        {
            TimeSpan time = TimeSpan.FromSeconds(30);
            //Delayed Jobs
            //Delayed jobs are executed only once too, but not immediately, after a certain time interval.
            var jobId = _backgroundJobClient.Schedule(() => _service.ExecuteMappingForAllDataSources(), time);

            return $"Job ID: {jobId}. Mapping for all data sources has been executed with {time.TotalSeconds} seconds delay.";
        }

        [HttpGet]
        [Route("IContinuousJob")]
        public string ContinuousJob()
        {
            //Fire - and - Forget Jobs
            //Fire - and - forget jobs are executed only once and almost immediately after creation.
            var parentjobId = _backgroundJobClient.Enqueue(() => _service.ExecuteMappingForAllDataSources());

            //Continuations
            //Continuations are executed when its parent job has been finished.
            _ = BackgroundJob.ContinueJobWith(parentjobId, () => _service.ExecuteMappingForAllDataSources());

            return "Welcome user in Continuos Job Demo!";
        }

        [HttpGet]
        [Route("IRecurringJob")]
        public string RecurringJobs()
        {
            //Recurring Jobs
            //Recurring jobs fire many times on the specified CRON schedule.
            _recurringJobManager.AddOrUpdate("jobId", () => _service.ExecuteMappingForAllDataSources(), Cron.Hourly);

            return $"Mapping for all data sources will be executed on Hourly basis.";
        }

        [HttpGet("HealthCheck")]
        public IActionResult HealthCheck()
        {
            return Ok(true);
        }
    }
}