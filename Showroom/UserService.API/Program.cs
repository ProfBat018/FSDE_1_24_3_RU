using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using UserService.API.Middlewares;
using UserService.API.Services.Classes;
using UserService.API.Services.Interfaces;
using UserService.Data.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<UserDbContext>(ops => 
    ops.UseSqlServer(builder.Configuration.GetConnectionString("Mac")));

builder.Services.AddSingleton<GlobalExceptionMiddleware>();
 
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapScalarApiReference();

app.MapControllers();
app.UseHttpsRedirection();

app.Run();

