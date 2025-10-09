using System.Reflection;
using System.Text;
using AuthApi.Abstractions.Repos;
using AuthApi.Application.Services.Classes;
using AuthApi.Application.Services.Interfaces;
using AuthApi.Application.Utils;
using AuthApi.Infrastructure.Contexts;
using AuthApi.Infrastructure.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        services.AddControllers(); // Добавляет поддержку контроллеров с помошью рефлексии. 

        services.AddSignalR();

        services.AddCors(policy =>
        {
            policy.AddPolicy("DefaultCorsPolicy", builder =>
            {
                builder.WithOrigins("http://localhost:3000", "http://localhost:5173", "https://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
        
        // Добавляю контекст базы данных с помощью Entity Framework Core и SQL Server.
        services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Mac")));
        
        services.AddScoped<IUserDbContext>(sp => sp.GetRequiredService<UserDbContext>());

        
        var cultures = new[] { "az-AZ", "en-US", "ru-RU" };

        services.AddRequestLocalization(ops =>
        {
            ops.SetDefaultCulture(cultures[0])
                .AddSupportedCultures(cultures);
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

                // Настройка для SignalR
                options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                        {
                            context.Token = accessToken;
                        }
                        
                        return Task.CompletedTask;
                    }
                };

                // options.Events.OnAuthenticationFailed = async context =>
                // {
                //     
                //     var req = context.Request;
                //     var res = context.Response;

                    // // если нужен refresh-токен из заголовка (лучше cookie)
                    // if (req.Headers.TryGetValue("refreshToken", out var hdr) && !string.IsNullOrWhiteSpace(hdr))
                    // {
                    //     // получаем сервисы корректно, через RequestServices
                    //     var tokenService = httpContext.RequestServices.GetRequiredService<ITokenService>();
                    //
                    //     var refreshToken = hdr.ToString();
                    //     var refreshed = await tokenService.RefreshTokenAsync(refreshToken);
                    //
                    //     if (refreshed is not null)
                    //     {
                    //         // вернём новые токены в заголовках (или в Set-Cookie)
                    //         res.Headers["accessToken"] = refreshed.AccessToken;
                    //         res.Headers["refreshToken"] = refreshed.RefreshToken;
                    //
                    //         // 200/204, чтобы фронт смог повторить запрос с новым access-токеном
                    //         res.StatusCode = StatusCodes.Status200OK;
                    //         return;
                    //     }
                    // }
                    //
                    // // если refresh не передан/невалиден — отдадим 401
                    // res.StatusCode = StatusCodes.Status401Unauthorized;
                // };
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