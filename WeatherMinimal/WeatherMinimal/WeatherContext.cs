using Microsoft.EntityFrameworkCore;

namespace WeatherMinimal;

public class WeatherContext : DbContext
{

    public DbSet<CityWeatherResult> Results { get; set; }
    public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
    {
        
    }


}