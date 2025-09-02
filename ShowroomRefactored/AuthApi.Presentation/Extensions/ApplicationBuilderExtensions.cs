using AuthApi.Infrastructure.Middlewares;
using Scalar.AspNetCore;

namespace AuthApi.Presentation.Extensions;


public static class ApplicationBuilderExtensions
{
    public static WebApplication UseApplicationMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
        app.UseMiddleware<GlobalExceptionMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapScalarApiReference();

        return app;

    }
}
