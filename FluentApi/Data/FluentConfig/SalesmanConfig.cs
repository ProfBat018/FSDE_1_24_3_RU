using FluentApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FluentApi.Data.FluentConfig;

public class SalesmanConfig : IEntityTypeConfiguration<Salesman>
{
    public void Configure(EntityTypeBuilder<Salesman> builder)
    {
        builder.HasKey(sm => sm.BadgeId);
        
        builder.Property(sm => sm.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(sm => sm.Surname)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(sm => sm.Salary)
            .IsRequired();
        
        builder.HasMany(sm => sm.Sales)
            .WithOne(s => s.SalesmanRef)
            .HasForeignKey(s => s.SalesmanBadgeId);
    }
}