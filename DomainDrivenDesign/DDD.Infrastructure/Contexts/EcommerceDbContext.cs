using System.Reflection;
using DDD.Domain.Models;
using Microsoft.EntityFrameworkCore;
using AttributeEntity = DDD.Domain.Models.Attribute;

namespace DDD.Infrastructure.Contexts;

public class EcommerceDbContext : DbContext
{
    public DbSet<AttributeEntity> Attributes { get; set; }
    public DbSet<AttributeValue> AttributeValues { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<HubObject> Hubs { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductWarehouse> ProductWarehouses { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }

    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> ops) : base(ops)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}