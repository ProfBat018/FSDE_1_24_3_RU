using ContactService.Application.Classes;
using ContactService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ContactService.Data;
using ContactService.Infrastructure.Messaging;

namespace ContactService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddOpenApi();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddDbContext<ContactDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddSingleton<IEventPublisher, RabbitMqPublisher>();

        services.AddScoped<IContactService, Application.Services.ContactService>();
        services.AddScoped<GrpcUserClient>();

        return services;
    }
}