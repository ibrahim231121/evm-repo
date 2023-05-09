using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Crossbones.ALPR.Common
{
    public static class LogHelper
    {
        public static string CreateLogMessage(HttpRequest httpRequest)
        {
            var headerDictionary=ExtractHeaderInformation(httpRequest);
            return $"[{httpRequest.Method}] request received from {httpRequest.HttpContext.Connection.RemoteIpAddress} at Url:{headerDictionary["Url"]} with TenantId:{headerDictionary["TenantId"]} UserId:{headerDictionary["UserId"]} UserAgent:{headerDictionary["User-Agent"]}";
        }
       
        private static IDictionary<string, object> ExtractHeaderInformation(HttpRequest httpRequest)
        {
            var ip = httpRequest.HttpContext.Connection.RemoteIpAddress;

            var headerDictionary = (new Dictionary<string, object>());
            foreach(var item in httpRequest.Headers)
                headerDictionary.Add(item.Key, item.Value);

            headerDictionary.Add("RemoteIpAddress", httpRequest.HttpContext.Connection.RemoteIpAddress.ToString());
            headerDictionary.Add("IPV4", ip.MapToIPv4().ToString());
            headerDictionary.Add("IPV6", ip.MapToIPv6().ToString());
            headerDictionary.Add("Url", httpRequest.GetDisplayUrl());

            if(!httpRequest.Headers.ContainsKey("User-Agent"))
                headerDictionary.Add("User-Agent", "N/A");

            if(!httpRequest.Headers.ContainsKey("UserId"))
                headerDictionary.Add("UserId", 0);

            if(!httpRequest.Headers.ContainsKey("TenantId"))
                headerDictionary.Add("TenantId", 0);

            return headerDictionary;
        }
    }
}
