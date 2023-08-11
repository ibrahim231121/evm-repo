using Crossbones.Modules.Common.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Crossbones.ALPR.Common.Log
{
    public static class LogRequest
    {
        public static string GetLogMessage(HttpRequest httpRequest, string message)
        {
            var headerDictionary = ExtractDataFromHeader(httpRequest);
            return $"[{httpRequest.Method}] Url:{headerDictionary["Url"]} TenantId:{headerDictionary["TenantId"]} UserId:{headerDictionary["UserId"]} IP:{headerDictionary["IPV4"]} UserAgent:{headerDictionary["User-Agent"]} Message:{message}";
        }
        public static void FillRequestInformation(HttpRequest httpRequest, IRequestInformation requestInfo)
        {
            var headerDictionary = ExtractDataFromHeader(httpRequest);
            requestInfo.UserId = Convert.ToInt64(headerDictionary["UserId"].ToString());
            requestInfo.TenantId = Convert.ToInt64(headerDictionary["TenantId"].ToString());
            requestInfo.TenantServiceId = Utility.GetFormattedTenantServiceId(requestInfo.TenantId, ServiceType.ALPR);
        }

        private static IDictionary<string, object> ExtractDataFromHeader(HttpRequest httpRequest)
        {
            var ip = httpRequest.HttpContext.Connection.RemoteIpAddress;

            var headerDictionary = (new Dictionary<string, object>());
            foreach (var item in httpRequest.Headers)
                headerDictionary.Add(item.Key, item.Value);

            if (ip != null)
            {
                headerDictionary.Add("IPV4", ip.MapToIPv4().ToString());
                headerDictionary.Add("IPV6", ip.MapToIPv6().ToString());
            }

            headerDictionary.Add("Url", httpRequest.GetDisplayUrl());

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
