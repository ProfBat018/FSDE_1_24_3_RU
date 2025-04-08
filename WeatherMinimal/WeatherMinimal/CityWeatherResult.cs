using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WeatherMinimal;

public class CityWeatherResult
{
    public Coord coord { get; set; }
    public Weather[] weather { get; set; }
    public string @base { get; set; }
    public Main main { get; set; }
    public int visibility { get; set; }
    public Wind wind { get; set; }
    public Rain rain { get; set; }
    public Clouds clouds { get; set; }
    public int dt { get; set; }
    public Sys sys { get; set; }
    public int timezone { get; set; }
    [Key]
    public int id { get; set; }
    public string name { get; set; }
    public int cod { get; set; }
}

public class Coord
{
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.NewGuid();
    public double lon { get; set; }
    public double lat { get; set; }

    public ICollection<CityWeatherResult> Results { get; set; }
}

public class Weather
{
    [JsonIgnore]
    [Key]
    public Guid IdKey { get; set; } = Guid.NewGuid();
    public int id { get; set; }
    public string main { get; set; }
    public string description { get; set; }
    public string icon { get; set; }
    public ICollection<CityWeatherResult> Results { get; set; }

}

public class Main
{
    [JsonIgnore]
    [Key]
    public Guid IdKey { get; set; } = Guid.NewGuid();
    public double temp { get; set; }
    public double feels_like { get; set; }
    public double temp_min { get; set; }
    public double temp_max { get; set; }
    public int pressure { get; set; }
    public int humidity { get; set; }
    public int sea_level { get; set; }
    public int grnd_level { get; set; }
    public ICollection<CityWeatherResult> Results { get; set; }

}

public class Wind
{
    [JsonIgnore]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public double speed { get; set; }
    public int deg { get; set; }
    public double gust { get; set; }
    public ICollection<CityWeatherResult> Results { get; set; }

}

public class Rain
{
    [JsonIgnore]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public double _h { get; set; }
    public ICollection<CityWeatherResult> Results { get; set; }

}

public class Clouds
{
    [JsonIgnore]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public int all { get; set; }
    public ICollection<CityWeatherResult> Results { get; set; }

}

public class Sys
{
    [JsonIgnore]
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string country { get; set; }
    public int sunrise { get; set; }
    public int sunset { get; set; }
    public ICollection<CityWeatherResult> Results { get; set; }

}

