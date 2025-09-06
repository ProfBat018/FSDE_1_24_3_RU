using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AttributeEntity = DDD.Domain.Models.Attribute;

namespace DDD.Infrastructure.FluentConfigurations;

public sealed class AttributeConfig : IEntityTypeConfiguration<AttributeEntity>
{
    public void Configure(EntityTypeBuilder<AttributeEntity> builder)
    {
        builder.ToTable("Attributes");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.AttributeName).IsRequired();

        builder.HasMany(a => a.AttributeValues)
               .WithOne(v => v.Attribute)
               .HasForeignKey(v => v.AttributeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}