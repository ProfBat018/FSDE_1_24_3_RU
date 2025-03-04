using FluentApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentApi.Data.FluentConfig;

public class FuelTypeConfig : IEntityTypeConfiguration<FuelType>
{
    public void Configure(EntityTypeBuilder<FuelType> builder)
    {
        builder.HasKey(ft => ft.Id);
        
        builder.Property(ft => ft.FuelTypeName)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(ft => ft.Cars)
            .WithOne(c => c.FuelType)
            .HasForeignKey(c => c.FuelTypeId);
    }
}