using AuthApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthApi.Infrastructure.Configurations;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfoTranslations>
{
    public void Configure(EntityTypeBuilder<UserInfoTranslations> builder)
    {
        builder.HasKey(ut => ut.TranslationId);
        
        builder.Property(ut => ut.LanguageCode)
            .IsRequired()
            .HasMaxLength(2);

        builder.Property(ut => ut.TranslatedInfo)
            .IsRequired();
    }
}