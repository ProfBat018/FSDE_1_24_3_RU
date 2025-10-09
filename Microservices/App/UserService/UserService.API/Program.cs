using UserService.API.Grpc;
using UserService.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(80);
});

builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddGrpc();

var app = builder.Build();
app.MapGrpcService<UserGrpcService>();

app.UseApplicationPipeline();

app.Run();