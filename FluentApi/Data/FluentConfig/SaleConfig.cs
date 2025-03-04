using FluentApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentApi.Data.FluentConfig;

public class SaleConfig : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.HasKey(sc => sc.SaleId);
        
        builder.Property(sc => sc.SaleDate)
            .IsRequired();

        builder.Property(sc => sc.SalePrice)
            .IsRequired();

        builder.HasOne(sc => sc.SalesmanRef)
            .WithMany(s => s.Sales)
            .HasForeignKey(sc => sc.SalesmanBadgeId);

        builder.HasOne(s => s.CarRef)
            .WithMany(c => c.Sales)
            .HasForeignKey(s => s.CarId);

    }
}