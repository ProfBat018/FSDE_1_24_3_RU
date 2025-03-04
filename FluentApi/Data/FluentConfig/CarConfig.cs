using FluentApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentApi.Data.FluentConfig;

public class CarConfig : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.HasKey(c => c.Id);
        
        builder.Property(c => c.Make)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(c => c.Model)
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(c => c.Color)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(c => c.ProductionDate)
            .IsRequired();
        
        builder.HasOne(c => c.CarType)
            .WithMany(ct => ct.Cars)
            .HasForeignKey(c => c.CarTypeId);
        
        builder.HasOne(c => c.FuelType)
            .WithMany(ft => ft.Cars)
            .HasForeignKey(c => c.FuelTypeId);
    }
}