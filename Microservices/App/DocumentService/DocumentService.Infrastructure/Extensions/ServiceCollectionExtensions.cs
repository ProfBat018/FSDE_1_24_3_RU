using DocumentService.Application.Classes;
using DocumentService.Application.Interfaces;
using DocumentService.Data;
using DocumentService.Infrastructure.Messaging;
using DocumentService.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DocumentDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("Default")));

        services.AddSingleton<IDocumentStorageService, MinioDocumentStorageService>();
        services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();
        services.AddScoped<DocumentUploadService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}