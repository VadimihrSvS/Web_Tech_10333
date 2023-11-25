using Microsoft.AspNetCore.Builder;
using AspNet_Projects.Middleware;

namespace AspNet_Projects.Extensions
{
    public static class AppExtensions
    {
        public static IApplicationBuilder UseFileLogging(this IApplicationBuilder app)
        => app.UseMiddleware<LogMiddleware>();
    }
}
