using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Data.Entities;

namespace UserService.Data.Configurations;

public class UserContactsConfiguration : IEntityTypeConfiguration<UserContacts>
{
    public void Configure(EntityTypeBuilder<UserContacts> builder)
    {
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.PhoneNumber).HasMaxLength(50);
        builder.Property(x => x.Email).HasMaxLength(256);
        builder.Property(x => x.PhoneVerified).IsRequired();
        builder.Property(x => x.EmailVerified).IsRequired();
    }
}