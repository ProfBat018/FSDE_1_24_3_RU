using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Data.Entities;

namespace UserService.Data.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ImagePath).IsRequired();
        builder.Property(x => x.Extension).HasMaxLength(10);
        builder.Property(x => x.SizeInBytes).IsRequired();
        builder.Property(x => x.UploadedAt).IsRequired();
    }
}