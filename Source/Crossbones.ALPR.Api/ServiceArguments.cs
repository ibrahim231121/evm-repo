using Crossbones.Logging;
using Crossbones.Modules.Api;
using Crossbones.Workers.Common;

namespace Crossbones.ALPR.Api
{
    public class ServiceArguments
    {
        internal ILogger _logger;
        internal ICommunicator _communicator;
        internal IHttpContextAccessorTenant _httpContext;

        public ServiceArguments(ILogger logger, ICommunicator communicator, IHttpContextAccessorTenant httpContext)
        {
            _logger = logger;
            _communicator = communicator;
            _httpContext = httpContext;
        }
    }
}
