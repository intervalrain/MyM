using Microsoft.AspNetCore.Builder;

using MyMoney.Infrastructure.Common.Middleware;

namespace MyMoney.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
    {
        app.UseMiddleware<EventualConsistencyMiddleware>();
        return app;
    }
}

