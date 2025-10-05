using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ImageService.Data.Entities;

namespace ImageService.Data.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<StoredImage>
{
    public void Configure(EntityTypeBuilder<StoredImage> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.ImagePath)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(i => i.ImagePath)
            .IsUnique();

        builder.Property(i => i.Extension)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(i => i.SizeInBytes)
            .IsRequired();

        builder.Property(i => i.UploadedAt)
            .IsRequired();

        builder.Property(i => i.CreatedAt).IsRequired();
        builder.Property(i => i.IsDeleted).HasDefaultValue(false);
    }
}