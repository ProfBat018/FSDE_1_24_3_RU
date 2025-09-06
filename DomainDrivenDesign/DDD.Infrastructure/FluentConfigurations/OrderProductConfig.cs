using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using DDD.Domain.Models;
namespace DDD.Infrastructure.FluentConfigurations;


public sealed class OrderProductConfig : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder.ToTable("OrderProducts");

        builder.HasKey(op => new { op.OrderId, op.ProductId });

        builder.Property(op => op.OrderId).IsRequired();
        builder.Property(op => op.ProductId).IsRequired();
        builder.Property(op => op.ProductCount).IsRequired();

        builder.HasOne<Order>()
               .WithMany()
               .HasForeignKey(op => op.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Product>()
               .WithMany()
               .HasForeignKey(op => op.ProductId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}