namespace DocumentService.Contracts.Events;

public record DocumentUploadedEvent(
    Guid DocumentId,
    Guid CustomerId,
    string DocumentType,
    string StoredFileName,
    DateTime UploadedAt
);