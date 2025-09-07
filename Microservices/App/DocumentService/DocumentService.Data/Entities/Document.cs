using DocumentService.Contracts.Enums;

namespace DocumentService.Data.Entities;

public class Document
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string DocumentType { get; set; } = default!;
    public string OriginalFileName { get; set; } = default!;
    public string StoredFileName { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public long FileSize { get; set; }
    public string InternalUrl { get; set; } = default!;
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastAccessedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }

    public DocumentStatus Status { get; set; } = DocumentStatus.Pending;
    public bool IsDeleted { get; set; } = false;
}