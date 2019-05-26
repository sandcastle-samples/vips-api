using Microsoft.AspNetCore.Builder;
using VipsApi.Middleware.ServerTime;

namespace VipsApi.Middleware
{
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Enables returning the x-server-time header.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <returns>The builder.</returns>
        public static IApplicationBuilder UseServerTime(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ServerTimeMiddleware>();
        }
    }
}
