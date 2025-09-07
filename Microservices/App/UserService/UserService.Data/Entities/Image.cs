namespace UserService.Data.Entities;

public class Image : BaseEntity
{
    public Guid Id { get; set; }
    public string ImagePath { get; set; }
    public string Extension { get; set; }
    public long SizeInBytes { get; set; }
    public DateTime UploadedAt { get; set; }
}