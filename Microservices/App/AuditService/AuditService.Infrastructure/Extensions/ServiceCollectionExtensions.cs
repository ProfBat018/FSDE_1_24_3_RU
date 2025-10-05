
using AuditService.Application.Interfaces;
using AuditService.Infrastructure.Messaging;
using AuditService.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IAuditLogRepository, MongoAuditLogRepository>();

        services.AddHostedService<RabbitMqSubscriber>();
        
        services.AddHealthChecks()
            .AddMongoDb(
                mongodbConnectionString: config.GetConnectionString("MongoDb"),
                name: "MongoDB",
                timeout: TimeSpan.FromSeconds(3));

        return services;
    }
}