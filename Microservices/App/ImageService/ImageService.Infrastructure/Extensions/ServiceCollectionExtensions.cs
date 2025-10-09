using ImageService.Application.Classes;
using ImageService.Application.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scalar.AspNetCore;
using ImageService.Data;

namespace ImageService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddOpenApi();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<IStoredImageService, StoredImageService>();
        services.AddSingleton<IMinioService, MinioService>();

        services.AddDbContext<ImageDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default")));


        return services;
    }
}