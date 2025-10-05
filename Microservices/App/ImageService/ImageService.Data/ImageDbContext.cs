using Microsoft.EntityFrameworkCore;
using ImageService.Data.Entities;

namespace ImageService.Data;

public class ImageDbContext : DbContext
{
    public ImageDbContext(DbContextOptions<ImageDbContext> options)
        : base(options)
    {
    }

    public DbSet<StoredImage> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ImageDbContext).Assembly);
    }
}