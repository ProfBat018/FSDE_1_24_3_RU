using System.Globalization;
using System.Reflection;
using System.Text;
using AuthApi.Abstractions.Repos;
using AuthApi.Application.Services.Classes;
using AuthApi.Application.Services.Interfaces;
using AuthApi.Application.Utils;
using AuthApi.Infrastructure.Contexts;
using AuthApi.Infrastructure.Filters;
using AuthApi.Infrastructure.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AuthApi.Infrastructure.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOpenApi(); // Добавляет OpenApi конфигурацию для документирования api. 
        services.AddControllers(ops =>
        {
            ops.Filters.Add<TranslateResultFilter>();
        }); // Добавляет поддержку контроллеров с помошью рефлексии. 

        // Добавляю контекст базы данных с помощью Entity Framework Core и SQL Server.
        services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Mac")));
        
        services.AddScoped<IUserDbContext>(sp => sp.GetRequiredService<UserDbContext>());

        
        var supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("ru"),
            new CultureInfo("az"),
            new CultureInfo("en")
        };


        services.AddRequestLocalization(ops =>
        {
            ops.DefaultRequestCulture = new RequestCulture("az");
            ops.SupportedCultures = supportedCultures;
            // ops.SupportedUICultures = supportedCultures;
            
            // Указываем, что за основу берём Accept-Language
            ops.RequestCultureProviders = new List<IRequestCultureProvider>
            {
                new AcceptLanguageHeaderRequestCultureProvider()
            };
        });
            
        // Регистрирую в DI контейнере сервисы с областью видимости на запрос.
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<TokenManager>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<EmailSender>();
        services.AddSingleton<GlobalExceptionMiddleware>();

        // Добавляю AutoMapper для автоматического маппинга объектов с помощью рефлексии.
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

   

        services
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Настраиваю аутентификацию с использованием JWT Bearer токенов.
        services.AddAuthentication(options =>
            {
                // Настройка схемы аутентификации по умолчанию.
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // Настройка схемы вызова по умолчанию.
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
                };

               
            });

        // Настраиваю авторизацию с политиками на основе ролей.
        services.AddAuthorization(ops =>
        {
            ops.AddPolicy("AdminPolicy", builder => builder.RequireRole("AppAdmin"));
            ops.AddPolicy("UserPolicy", builder => builder.RequireRole("AppAdmin", "AppUser"));
        });

        return services;
    }
}