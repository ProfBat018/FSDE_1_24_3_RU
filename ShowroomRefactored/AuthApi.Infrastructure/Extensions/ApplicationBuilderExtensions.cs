using AuthApi.Application.Hubs;
using AuthApi.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
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

        app.UseCors("DefaultCorsPolicy"); // Включение CORS с политикой "DefaultCorsPolicy".)
        app.MapHub<NotificationHub>("hubs/notification"); // Маршрут для SignalR хаба уведомлений.
        
        // Переадресация HTTP запросов на HTTPS.
        app.UseHttpsRedirection();
        
        // Маршрутизация запросов к соответствующим контроллерам.
        app.MapControllers();
        
        // Глобальный обработчик исключений.
        app.UseMiddleware<GlobalExceptionMiddleware>();

        app.UseRequestLocalization();
        // Включение middleware для аутентификации и авторизации.
        app.UseAuthentication();
        app.UseAuthorization();

        // Добавление поддержки Scalar API.
        app.MapScalarApiReference();

        return app;

    }
}
