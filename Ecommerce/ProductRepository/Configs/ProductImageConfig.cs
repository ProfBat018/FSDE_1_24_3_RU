using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductRepository.Models;

namespace ProductRepository.Configs;

public sealed class ProductImageConfig : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.ToTable("ProductImages");

        builder.HasKey(pi => pi.ImageName);

        builder.Property(pi => pi.ImageName).IsRequired();
        builder.Property(pi => pi.ProductId).IsRequired();
        builder.Property(pi => pi.ImagePath).IsRequired();
        builder.Property(pi => pi.IsMain).IsRequired();

        builder.HasOne(pi => pi.Product)
               .WithMany(p => p.ProductImages)
               .HasForeignKey(pi => pi.ProductId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}