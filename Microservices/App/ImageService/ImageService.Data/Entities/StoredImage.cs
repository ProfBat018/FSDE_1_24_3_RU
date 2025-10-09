using System.ComponentModel.DataAnnotations;

namespace ImageService.Data.Entities;

public class StoredImage : BaseEntity
{
    public Guid Id { get; set; }
    public string ImagePath { get; set; } = default!;
    public string Extension { get; set; } = default!;
    public long SizeInBytes { get; set; }
    public DateTime UploadedAt { get; set; }
}