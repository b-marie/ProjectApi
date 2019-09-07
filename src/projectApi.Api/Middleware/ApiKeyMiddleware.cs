using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectApi.Api.Middleware
{
    public class ApiKeyMiddleware
    {
        private static readonly string allowedApiKeys = Environment.GetEnvironmentVariable("APIKeyList");
        private readonly List<string> allowedApiKeysList = allowedApiKeys.Split(',').ToList<string>();
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var headers = context.Request.Headers;
            var apiKey = headers["ProjectApiKey"];
            if (string.IsNullOrEmpty(apiKey) || !allowedApiKeysList.Contains(apiKey))
            {
                throw new UnauthorizedAccessException();
            }

            await _next(context);
        }
    }
}
