using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDD.Domain.Models;

namespace DDD.Infrastructure.FluentConfigurations;

public sealed class HubObjectConfig : IEntityTypeConfiguration<HubObject>
{
    public void Configure(EntityTypeBuilder<HubObject> builder)
    {
        builder.ToTable("Hubs");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.HubName).IsRequired();
        builder.Property(h => h.ProductWarehouseId).IsRequired();

        builder.HasOne(h => h.ProductWarehouse)
               .WithMany()
               .HasForeignKey(h => h.ProductWarehouseId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(h => h.ProductWarehouseId).IsUnique();
    }
}