using Crossbones.Modules.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crossbones.ALPR.Api.State
{
    [Route("State")]
    public class StateController : BaseController
    {
        IStateService stateService;
        public StateController(ApiParams feature, IStateService _stateService) : base(feature)
        {
            stateService = _stateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stateList = await stateService.GetAll();

            return StatusCode(StatusCodes.Status200OK, stateList.Select(x => new { id = x.StateId, label = x.StateName }));
        }
    }
}