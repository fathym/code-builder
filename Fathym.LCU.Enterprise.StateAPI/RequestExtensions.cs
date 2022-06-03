using System;
using System.Linq;
using System.Net.Http;

namespace Fathym.LCU.Enterprise.StateAPI
{
    public static class RequestExtensions
    {
        public static string GetCurrentUser(this HttpRequestMessage request)
        {
            var defaultUser = Environment.GetEnvironmentVariable("LCU_USER");

            if (request.Headers.Contains("x-ms-client-principal-id"))
                return request.Headers.GetValues("x-ms-client-principal-id")?.FirstOrDefault() ?? defaultUser;
            else
                return defaultUser;
        }
    }
}