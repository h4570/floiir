using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;

namespace WebApi.Misc.Http
{
    public static class HttpUtilities
    {

        /// <summary>
        /// Returns host from request headers.
        /// Examples: "localhost", "google.com"
        /// </summary>
        public static string GetHostFromRequestHeaders(IHeaderDictionary headers)
        {
            headers.TryGetValue("Origin", out StringValues originValues);
            string dirty;
            if (originValues.Count > 0)
                dirty = originValues[0];
            else throw new Exception("Origin was not found in request headers");
            var clean = new Uri(dirty).Host;
            return clean.ToString();
        }

    }
}
