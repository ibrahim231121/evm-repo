using Crossbones.ALPR.Common.Log;
using Crossbones.Common;
using Crossbones.Logging;
using Crossbones.Modules.Api;
using Crossbones.Modules.Business;
using Crossbones.Modules.Common.Queryables;
using Crossbones.Workers.Common;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Crossbones.ALPR.Api
{
    public class ServiceBase
    {
        internal readonly ILogger _logger;
        internal readonly ICommunicator _communicator;
        internal readonly IHttpContextAccessorTenant _httpContextAccessor;
        internal readonly IHeaderDictionary _requestHeaderInformation;
        public ServiceBase(ServiceArguments args) => (_logger, _communicator, _httpContextAccessor, _requestHeaderInformation) = (args._logger, args._communicator, args._httpContext, args._httpContext.HttpContext.Request.Headers);
        protected Task<Unit> Execute<T>(T command) where T : class
        {
            if (command is IRequestInformation requestInfo)
            {
                LogRequest.FillRequestInformation(_httpContextAccessor.HttpContext.Request, requestInfo, _httpContextAccessor.TenantId);
            }
            else
            {
                foreach (var item in (command as ChainCommand).Commands)
                    LogRequest.FillRequestInformation(_httpContextAccessor.HttpContext.Request, item as IRequestInformation, _httpContextAccessor.TenantId);
            }
            return _communicator.SendRequest<Unit>(command, System.Threading.CancellationToken.None);
        }
        protected Task<TResponse> Inquire<TResponse>(object request) where TResponse : class
        {
            if (request is IRequestInformation requestInfo)
            {
                LogRequest.FillRequestInformation(_httpContextAccessor.HttpContext.Request, requestInfo, _httpContextAccessor.TenantId);
            }
            else { }
            return _communicator.SendRequest<TResponse>(request, System.Threading.CancellationToken.None);
        }
        protected void Broadcast<T>(T broadcast) => _communicator.Broadcast(broadcast, System.Threading.CancellationToken.None);
        protected void Send<T>(T message) => _communicator.Send(message, System.Threading.CancellationToken.None);
        protected long GetTenantId()
        {
            return _requestHeaderInformation.ContainsKey("TenantId") ? Convert.ToInt64(_requestHeaderInformation["TenantId"].ToString()) : 0;
        }
        protected GridFilter GetGridFilter()
        {
            bool isGridFilter = _httpContextAccessor.HttpContext.Request.Headers.Where(x => x.Key == "GridFilter").Count() > 0;

            if (isGridFilter)
            {
                GridFilter filter = null;
                Microsoft.Extensions.Primitives.StringValues HeaderGridFilter;
                _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("GridFilter", out HeaderGridFilter);
                filter = JsonConvert.DeserializeObject<GridFilter>(HeaderGridFilter);
                return filter;
            }

            return null;
        }

        protected GridSort GetGridSort()
        {
            bool isGridFilter = _httpContextAccessor.HttpContext.Request.Headers.Where(x => x.Key == "GridSort").Count() > 0;

            if (isGridFilter)
            {
                GridSort sort = null;
                Microsoft.Extensions.Primitives.StringValues HeaderGridSort;
                _httpContextAccessor.HttpContext.Request.Headers.TryGetValue("GridSort", out HeaderGridSort);
                sort = JsonConvert.DeserializeObject<GridSort>(HeaderGridSort);
                return sort;
            }

            return null;
        }
    }
}