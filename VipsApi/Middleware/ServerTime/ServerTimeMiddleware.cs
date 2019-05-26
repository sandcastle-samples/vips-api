using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace VipsApi.Middleware.ServerTime
{
    public class ServerTimeMiddleware
    {
        readonly RequestDelegate _next;

        public ServerTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        [UsedImplicitly]
        public async Task Invoke(HttpContext context)
        {
            long startTime = DateTime.UtcNow.Ticks;

            context.Response.OnStarting(state =>
            {
                var watch = TimeSpan.FromTicks(DateTime.UtcNow.Ticks - startTime);

                var responseContext = (HttpContext)state;
                AddHeader(responseContext, (long)watch.TotalMilliseconds);

                return Task.FromResult(0);
            }, context);

            await _next(context);
        }

        static void AddHeader(HttpContext context, long milliseconds)
        {
            context.Response.Headers.Add("X-ServerTime", $"{milliseconds:N0}ms");
        }
    }
}
