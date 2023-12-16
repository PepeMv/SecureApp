using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace Conexion.Middlewares
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly int _limit;
        private readonly TimeSpan _interval;

        public RateLimitMiddleware(RequestDelegate next, IMemoryCache cache, int limit, TimeSpan interval)
        {
            _next = next;
            _cache = cache;
            _limit = limit;
            _interval = interval;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress.ToString();

            var rateLimitKey = $"{ipAddress}_RateLimit";

            if (_cache.TryGetValue(rateLimitKey, out int requestCount))
            {
                if (requestCount >= _limit)
                {
                    context.Response.StatusCode = 429;
                    await context.Response.WriteAsync("Rate limit exceeded. Please try again later.");
                    return;
                }
            }

            _cache.Set(rateLimitKey, requestCount + 1, _interval);

            await _next(context);
        }
    }

    public static class RateLimitMiddlewareExtensions
    {
        public static IApplicationBuilder UseRateLimitMiddleware(this IApplicationBuilder builder, int limit, TimeSpan interval)
        {
            return builder.UseMiddleware<RateLimitMiddleware>(limit, interval);
        }
    }
}
