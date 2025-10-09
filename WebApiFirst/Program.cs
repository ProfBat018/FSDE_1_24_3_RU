// представлитель паттерна Builder, который нужен для того чтобы 
// строить сложные объекты. 

using WebApiFirst;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);  


// Services - это IServiceCollection, который хранит в себе ваши сервисы. 
// Так работает ваш IOC контейнер, тот самый контейнер из библиотеки 
// SimpleInjector который мы использовали в WPF
builder.Services.AddOpenApi();


WebApplication app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// middleware, который отвечает за переброс с http на https. Во время локальной 
// разработки для этого у вас должен быть самоподписанный сертификат, иначе это не 
// будет работать 
app.UseHttpsRedirection();


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
