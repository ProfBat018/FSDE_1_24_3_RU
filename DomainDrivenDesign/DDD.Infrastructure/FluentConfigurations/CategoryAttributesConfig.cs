using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDD.Domain.Models;

namespace DDD.Infrastructure.FluentConfigurations;


public sealed class CategoryAttributesConfig : IEntityTypeConfiguration<CategoryAttributes>
{
    public void Configure(EntityTypeBuilder<CategoryAttributes> builder)
    {
        builder.ToTable("CategoryAttributes");

        builder.HasKey(ca => new { ca.CategoryId, ca.AttributeId });

        builder.Property(ca => ca.CategoryId).IsRequired();
        builder.Property(ca => ca.AttributeId).IsRequired();

        builder.HasOne(ca => ca.Category)
               .WithMany(c => c.CategoryAttributes)
               .HasForeignKey(ca => ca.CategoryId)
               .HasPrincipalKey(c => c.CategoryName)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ca => ca.Attribute)
               .WithMany(a => a.CategoryAttributes)
               .HasForeignKey(ca => ca.AttributeId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(ca => new { ca.CategoryId, ca.AttributeId }).IsUnique();
    }
}