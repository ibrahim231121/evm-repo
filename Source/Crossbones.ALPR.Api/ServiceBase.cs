using Crossbones.ALPR.Common.Log;
using Crossbones.Common;
using Crossbones.Logging;
using Crossbones.Modules.Api;
using Crossbones.Modules.Business;
using Crossbones.Modules.Common.Exceptions;
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
        public ServiceBase(ServiceArguments args) => (_logger, _communicator, _httpContextAccessor) = (args._logger, args._communicator, args._httpContext);
        protected Task<Unit> Execute<T>(T command) where T : class
        {
            if (command is IRequestInformation requestInfo)
            {
                LogRequest.FillRequestInformation(_httpContextAccessor.HttpContext.Request, requestInfo);
            }
            else
            {
                foreach (var item in (command as ChainCommand).Commands)
                    LogRequest.FillRequestInformation(_httpContextAccessor.HttpContext.Request, item as IRequestInformation);
            }
            return _communicator.SendRequest<Unit>(command, CancellationToken.None);
        }
        protected Task<TResponse> Inquire<TResponse>(object request) where TResponse : class
        {
            if (request is IRequestInformation requestInfo)
            {
                LogRequest.FillRequestInformation(_httpContextAccessor.HttpContext.Request, requestInfo);
            }
            return _communicator.SendRequest<TResponse>(request, CancellationToken.None);
        }
        protected void ThorwIfNotValidValueObject(long Id, string valueName)
        {
            if (Id < 0)
                throw new ValidationFailed($"{valueName} cannot be negative.");
        }
        protected void Broadcast<T>(T broadcast) => _communicator.Broadcast(broadcast, CancellationToken.None);
        protected void Send<T>(T message) => _communicator.Send(message, CancellationToken.None);
        protected long GetTenantId()
        {
            var _requestHeaderInformation = _httpContextAccessor.HttpContext.Request.Headers;
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