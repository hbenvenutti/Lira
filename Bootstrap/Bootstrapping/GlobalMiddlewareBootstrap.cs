using Lira.Bootstrap.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Lira.Bootstrap.Bootstrapping;

public static class GlobalMiddlewareBootstrap
{
    public static void ConfigureGlobalMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
    }
}
