using DocumentService.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace DocumentService.Infrastructure.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionMiddleware>();
    }
}