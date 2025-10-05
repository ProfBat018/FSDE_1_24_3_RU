using AuthApi.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;

namespace AuthApi.Infrastructure.Extensions;


public static class ApplicationBuilderExtensions
{
    public static WebApplication UseApplicationMiddleware(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi(); // Добавляет маршрут для доступа к OpenApi документации.
        }
        
        
        // Переадресация HTTP запросов на HTTPS.
        app.UseHttpsRedirection();
        
        // Маршрутизация запросов к соответствующим контроллерам.
        app.MapControllers();
        
        // Глобальный обработчик исключений.
        app.UseMiddleware<GlobalExceptionMiddleware>();

        // Включение middleware для аутентификации и авторизации.
        app.UseAuthentication();
        app.UseAuthorization();

        // Добавление поддержки Scalar API.
        app.MapScalarApiReference();

        var locOptions = app.Services
            .GetRequiredService<IOptions<RequestLocalizationOptions>>()
            .Value;

        app.UseRequestLocalization(locOptions);
        
        return app;

    }
    
    
}
