using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDD.Domain.Models;

namespace DDD.Infrastructure.FluentConfigurations;


public sealed class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.UserId).IsRequired();
        builder.Property(o => o.HubId).IsRequired();
        builder.Property(o => o.TotalPrice).IsRequired();

        builder.HasOne(o => o.Hub)
               .WithMany()
               .HasForeignKey(o => o.HubId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}