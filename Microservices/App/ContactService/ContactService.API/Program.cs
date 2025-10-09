using ContactService.Application.Classes;
using ContactService.Infrastructure.Extensions;
using UserService.Grpc;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddGrpcClient<UserGrpc.UserGrpcClient>(o =>
{
    o.Address = new Uri("http://contactservice:80"); 
});


builder.Services.AddScoped<GrpcUserClient>();

var app = builder.Build();

app.UseApplicationPipeline();

app.Run();