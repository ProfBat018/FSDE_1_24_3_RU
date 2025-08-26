using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AttributeEntity = ProductRepository.Models.Attribute;

namespace ProductRepository.Configs;

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