using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductRepository.Models;

namespace ProductRepository.Configs;

public sealed class ProductWarehouseConfig : IEntityTypeConfiguration<ProductWarehouse>
{
    public void Configure(EntityTypeBuilder<ProductWarehouse> builder)
    {
        builder.ToTable("ProductWarehouses");

        builder.HasKey(pw => pw.Id);

        builder.Property(pw => pw.ProductId).IsRequired();
        builder.Property(pw => pw.WarehouseId).IsRequired();
        builder.Property(pw => pw.Count).IsRequired();
        builder.Property(pw => pw.Price).IsRequired();

        builder.HasOne(pw => pw.Product)
               .WithMany()
               .HasForeignKey(pw => pw.ProductId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(pw => pw.Warehouse)
               .WithMany()
               .HasForeignKey(pw => pw.WarehouseId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(pw => new { pw.ProductId, pw.WarehouseId }).IsUnique();
    }
}