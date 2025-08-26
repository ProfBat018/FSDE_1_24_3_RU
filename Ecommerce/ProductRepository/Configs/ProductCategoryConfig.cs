using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductRepository.Models;

namespace ProductRepository.Configs;

public sealed class ProductCategoryConfig : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.ToTable("ProductCategories");

        builder.HasKey(pc => new { pc.ProductId, pc.CategoryId });

        builder.Property(pc => pc.ProductCategoryId).IsRequired();
        builder.Property(pc => pc.ProductId).IsRequired();
        builder.Property(pc => pc.CategoryId).IsRequired();

        builder.HasOne(pc => pc.Product)
               .WithMany(p => p.ProductCategories)
               .HasForeignKey(pc => pc.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pc => pc.Category)
               .WithMany(c => c.ProductCategories)
               .HasForeignKey(pc => pc.CategoryId)
               .HasPrincipalKey(c => c.CategoryName)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(pc => new { pc.ProductId, pc.CategoryId }).IsUnique();
    }
}