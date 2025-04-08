using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WeatherMinimal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<WeatherContext>(ops => ops.UseSqlServer(
    builder.Configuration.GetConnectionString("WeatherDb")));

var app = builder.Build();


app.MapGet("weather/{city}", async (string city, WeatherContext context) =>
{
    var client = new HttpClient();

    var httpRequest = new HttpRequestMessage(HttpMethod.Get,
        $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=5191fee1957155f779bfd029a4a97e18&units=metric");

    var response = await client.SendAsync(httpRequest);
    
    if (response.IsSuccessStatusCode)
    {
        var stream = await response.Content.ReadAsStreamAsync();

        var weatherResult = await JsonSerializer.DeserializeAsync<CityWeatherResult>(stream);
        
        await context.Results.AddAsync(weatherResult);
        
        await context.SaveChangesAsync();
        
        return Results.Ok(weatherResult);
    }
    else
    {
        return Results.InternalServerError("Error fetching weather data");
    }

    

}).WithName("GetWeather")
    .WithOpenApi()
    .Produces<string>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithTags("Weather");

app.MapOpenApi();
app.MapScalarApiReference();

app.UseHttpsRedirection();

app.Run();