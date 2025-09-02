using AuthApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthApi.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        
        var name = builder.Property(u => u.Name);
        name.IsRequired();
        name.HasMaxLength(30);
        
        var surname = builder.Property(u => u.Surname);
        surname.IsRequired();
        surname.HasMaxLength(50);
        
        var email = builder.Property(u => u.Email);
        email.IsRequired();
        email.HasMaxLength(255);
        
        var password = builder.Property(u => u.Password);
        password.IsRequired();
        
        var isConfirmed = builder.Property(u => u.IsConfirmed);
        isConfirmed.HasDefaultValue(false);
    }
}