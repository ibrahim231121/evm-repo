using Crossbones.Modules.Common.Configuration;
using Crossbones.Modules.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Crossbones.ALPR.Common.Log
{
    public static class LogRequest
    {
        public static string GetLogMessage(HttpRequest httpRequest, string message)
        {
            var data = ExtractDataFromHeader(httpRequest);
            return $"Action: {data["Method"]} URL: {data["Url"]} TenantId: {data["TenantId"]} UserId: {data["UserId"]} IP: {data["IPV4"]} UserAgent: {data["User-Agent"]} Message: {message}";
        }
        public static void FillRequestInformation(HttpRequest httpRequest, IRequestInformation requestInfo, long tenantId)
        {
            //var data = ExtractDataFromHeader(httpRequest);
            //var tenantId = Convert.ToInt64(data["TenantId"].ToString());
            requestInfo.TenantServiceId = Utility.GetFormattedTenantServiceId(tenantId, ServiceType.ALPR);

            if (tenantId <= 0)
            {
                throw new RecordNotFound("TenantId not found in HTTP header!");
            }
        }
        private static IDictionary<string, object> ExtractDataFromHeader(HttpRequest httpRequest)
        {
            var ip = httpRequest.HttpContext.Connection.RemoteIpAddress;

            var headerDictionary = (new Dictionary<string, object>());
            foreach (var item in httpRequest.Headers)
                headerDictionary.Add(item.Key, item.Value);

            if (headerDictionary.ContainsKey("tenantid"))
            {
                var temp = headerDictionary["tenantid"];
                headerDictionary.Remove("tenantid");
                headerDictionary.Add("TenantId", temp);
            }
            headerDictionary.Add("IPV4", ip.MapToIPv4().ToString());
            headerDictionary.Add("IPV6", ip.MapToIPv6().ToString());
            headerDictionary.Add("Url", httpRequest.GetDisplayUrl());
            headerDictionary.Add("Method", httpRequest.Method);

            if (!httpRequest.Headers.ContainsKey("User-Agent"))
                headerDictionary.Add("User-Agent", "N/A");

            if (!httpRequest.Headers.ContainsKey("UserId"))
                headerDictionary.Add("UserId", 0);

            if (!httpRequest.Headers.ContainsKey("TenantId"))
                headerDictionary.Add("TenantId", 0);

            return headerDictionary;
        }
    }
}
