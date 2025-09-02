using System.Reflection;
using System.Text;
using AuthApi.Application.Services.Classes;
using AuthApi.Application.Services.Interfaces;
using AuthApi.Infrastructure.Contexts;
using AuthApi.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuthApi.Presentation.Extensions;


public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddControllers();

        services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Mac")));

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<EmailSender>();
        services.AddSingleton<GlobalExceptionMiddleware>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
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

        services.AddAuthorization(ops =>
        {
            ops.AddPolicy("AdminPolicy", builder => builder.RequireRole("AppAdmin"));
            ops.AddPolicy("UserPolicy", builder =>  builder.RequireRole("AppAdmin", "AppUser"));
        });

        return services;
    }
}