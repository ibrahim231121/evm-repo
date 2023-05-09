using Crossbones.ALPR.Common.Log;
using Crossbones.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Crossbones.ALPR.Api
{
    public class LogVisitor : IAsyncActionFilter
    {
        readonly ILogger _log;
        public LogVisitor(ILogger log) => _log = log ?? throw new ArgumentNullException(nameof(log));
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var t0 = next();
            var msg = LogRequest.GetLogMessage(context.HttpContext.Request, "Request received");
            _log.Info(msg);
            await t0;
        }
    }
    public class LogVisitorAttribute : TypeFilterAttribute
    {
        public LogVisitorAttribute() : base(typeof(LogVisitor)) => Arguments = new object[] { };
    }
}
