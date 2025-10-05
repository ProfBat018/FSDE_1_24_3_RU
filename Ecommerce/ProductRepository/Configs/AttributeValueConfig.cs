using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductRepository.Models;

namespace ProductRepository.Configs;

public sealed class AttributeValueConfig : IEntityTypeConfiguration<AttributeValue>
{
    public void Configure(EntityTypeBuilder<AttributeValue> builder)
    {
        builder.ToTable("AttributeValues");

        builder.HasKey(v => new { v.AttributeId, v.Value });

        builder.Property(v => v.AttributeId).IsRequired();
        builder.Property(v => v.Value).IsRequired();

        builder.HasOne(v => v.Attribute)
               .WithMany(a => a.AttributeValues)
               .HasForeignKey(v => v.AttributeId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}