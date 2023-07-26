using Crossbones.Modules.Api;
using Crossbones.Modules.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Crossbones.ALPR.Api.Health
{
    [Route("[controller]")]
    public class HealthController : ApiController
    {
        public HealthController(ApiParams feature) : base(feature) { }

        [HttpGet]
        public async Task<IActionResult> Status()
        {
            return Ok("ALPR service is running");
        }
    }
}