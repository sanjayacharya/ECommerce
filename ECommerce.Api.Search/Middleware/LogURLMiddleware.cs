using System.Runtime.CompilerServices;

namespace ECommerce.Api.Search.Middleware
{
    public class LogURLMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogURLMiddleware> _logger;
        public LogURLMiddleware(RequestDelegate next, ILogger<LogURLMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext contexxt)
        {
            _logger.LogInformation($"Request Url:{Microsoft.AspNetCore.Http.Extensions.UriHelper.GetDisplayUrl(contexxt.Request)}");
            await this._next(contexxt);
        }
    }
    public static class LogURLMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogUrl(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LogURLMiddleware>();
        }
    }
}
