using Crossbones.Logging;
using Crossbones.Modules.Common.Pagination;
using Crossbones.Modules.Api;
using Crossbones.Modules.Api.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Crossbones.Modules.Common.AuditLogs;
using Crossbones.Modules.Common.Configuration;
using Crossbones.Modules.Common;

namespace Crossbones.ALPR.Api
{
    [LogVisitor]
    [AuditLogVisitor(nameof(ServiceType.ALPR))]
    public abstract class BaseController : ApiController
    {
        protected BaseController(ApiParams feature) : base(feature)
        {
        }
        protected IActionResult Created(object id)
        {
            var url = $"{this.Request?.Scheme}://{this.Request?.Host}{this.Request?.Path}/{id}";
            return Created(url, id);
        }

        public string baseUrl
        {
            get
            {
                var obj = this.Request?.HttpContext?.Request;
                return $"{obj?.Scheme ?? ""}://{obj?.Host.ToString() ?? ""}{obj?.PathBase ?? ""}";
            }
        }

        protected ILogger Log => base._logger;
        protected void LogRequestInfo() => Log.Info($"Request recieved from {Request?.HttpContext.Connection.RemoteIpAddress.ToString() ?? "none"} at {ControllerContext?.HttpContext?.Request.Path.ToString() ?? "none"}");
        protected IActionResult MethodNotAllowed(string desciption = "")
        {
            return StatusCode(StatusCodes.Status405MethodNotAllowed, desciption);
        }
        protected IActionResult MethodNotFound(string desciption = "")
        {
            return new MethodNotFound();
        }
        public string GetUserAgent()
        {
            return Request?.Headers["User-Agent"].ToString() ?? "";
        }
        protected System.Net.IPAddress GetIPAddress()
        {
            return HttpContext?.Connection?.RemoteIpAddress ?? null;
        }

        protected IActionResult PagedResult<T>(PagedResponse<T> res) where T : class
        {
            Response.Headers.Add("Total", res.Total.ToString());
            Response.Headers.Add("QoS", $"TimeTaken: {res.QoS?.TimeTaken}, Hops: {res.QoS?.Hops}");
            return Ok(res.Data);
        }
        protected IActionResult PaginatedOk<T>(PageResponse<T> result)
        {
            if (result == null || Response == null)
            {
                return Ok("");
            }
            else
            {
                Response.Headers.Add("X-Total-Count", result.TotalCount.ToString());
                Response.Headers.Add("Access-Control-Expose-Headers", "X-Total-Count");

                return Ok(result.Items);
            }
        }
    }

    [DefaultStatusCode(404)]
    public class MethodNotFound : StatusCodeResult
    {
        private const int DefaultStatusCode = StatusCodes.Status404NotFound;

        /// <summary>
        /// Creates a new <see cref="Microsoft.AspNetCore.Mvc.NotFoundResult"/> instance.
        /// </summary>
        public MethodNotFound() : base(DefaultStatusCode)
        {
        }
    }
}
