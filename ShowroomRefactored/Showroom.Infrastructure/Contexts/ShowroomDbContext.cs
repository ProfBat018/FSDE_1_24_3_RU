using Microsoft.EntityFrameworkCore;
using Showroom.Data.Models;
using Attribute = Showroom.Data.Models.Attribute;
namespace Showroom.Infrastructure.Contexts;

public class ShowroomDbContext : DbContext
{

    public DbSet<Attribute> Attributes { get; set; }
    public DbSet<AttributeValue> AttributeValues { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<CarCategory> CarCategories { get; set; }
    public DbSet<CarImage> Images { get; set; }
    public DbSet<CarOrder> CarOrders { get; set; }
    public DbSet<CarWarehouse> CarWarehouses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryAttributes> CategoryAttributes { get; set; }
    public DbSet<HubObject> HubObjects { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    
    
    public ShowroomDbContext(DbContextOptions<ShowroomDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var attribute = modelBuilder.Entity<Attribute>();
        var attributeValue = modelBuilder.Entity<AttributeValue>();
        var car = modelBuilder.Entity<Car>();
        var carCategory = modelBuilder.Entity<CarCategory>();
        var carImage = modelBuilder.Entity<CarImage>();
        var carOrder = modelBuilder.Entity<CarOrder>();
        var carWarehouse = modelBuilder.Entity<CarWarehouse>();
        var category = modelBuilder.Entity<Category>();
        var categoryAttributes = modelBuilder.Entity<CategoryAttributes>();
        var hubObject = modelBuilder.Entity<HubObject>();
        var order = modelBuilder.Entity<Order>();
        var vendor = modelBuilder.Entity<Vendor>();
        var warehouse = modelBuilder.Entity<Warehouse>();
        // Primary Keys
        attribute.HasKey(a => a.Id);
        attributeValue.HasKey(av => av.Value);
        car.HasKey(c => c.Id);
        carCategory.HasKey(cc => cc.CarCategoryId);
        carImage.HasKey(ci => ci.ImageName);
        carOrder.HasKey(co => new { co.CarId, co.OrderId });
        carWarehouse.HasKey(cw => cw.Id);
        category.HasKey(c => c.CategoryName);
        categoryAttributes.HasKey(ca => new { ca.CategoryId, ca.AttributeId });
        hubObject.HasKey(ho => ho.Id);
        order.HasKey(o => o.Id);
        vendor.HasKey(v => v.Id);
        warehouse.HasKey(w => w.Id);
        
        
        
        
    }
}