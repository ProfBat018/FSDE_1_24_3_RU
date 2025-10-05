using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDD.Domain.Models;

namespace DDD.Infrastructure.FluentConfigurations;

public sealed class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.ProductName).IsRequired();
        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.VendorId).IsRequired();

        builder.HasOne(p => p.Vendor)
               .WithMany(v => v.Products)
               .HasForeignKey(p => p.VendorId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}