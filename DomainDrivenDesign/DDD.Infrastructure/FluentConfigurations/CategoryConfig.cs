using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DDD.Domain.Models;

namespace DDD.Infrastructure.FluentConfigurations;


public sealed class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(c => c.CategoryName);

        builder.Property(c => c.CategoryName).IsRequired();
        builder.Property(c => c.ParentCategoryName).IsRequired(false);

        builder.HasOne<Category>()
               .WithMany()
               .HasForeignKey(c => c.ParentCategoryName)
               .HasPrincipalKey(c => c.CategoryName)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => c.ParentCategoryName);
    }
}