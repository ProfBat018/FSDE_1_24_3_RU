using AuthApi.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

app.UseApplicationMiddleware();
app.Run();