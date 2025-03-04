using CodeFirst.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CodeFirst.Data.Contexts;

public class ShowroomContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarType> CarTypes { get; set; }
    public DbSet<FuelType> FuelTypes { get; set; }
    public DbSet<Salesman> Salesmen { get; set; }
    public DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        // optionsBuilder.UseLazyLoadingProxies();
        
        var connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
            .GetConnectionString("Default");
        
        optionsBuilder.UseSqlServer(connectionString);
    }
}