using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductRepository.Models;

namespace ProductRepository.Configs;

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