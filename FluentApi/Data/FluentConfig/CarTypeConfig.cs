using FluentApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentApi.Data.FluentConfig;

public class CarTypeConfig : IEntityTypeConfiguration<CarType>
{
    public void Configure(EntityTypeBuilder<CarType> builder)
    {
        builder.HasKey(ct => ct.Id);
        
        builder.Property(ct => ct.CarTypeName)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasMany(ct => ct.Cars)
            .WithOne(c => c.CarType)
            .HasForeignKey(c => c.CarTypeId);
    }
}