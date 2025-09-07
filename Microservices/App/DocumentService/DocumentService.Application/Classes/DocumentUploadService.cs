using AutoMapper;
using DocumentService.Application.Interfaces;
using DocumentService.Contracts.DTOs;
using DocumentService.Contracts.Enums;
using DocumentService.Contracts.Events;
using DocumentService.Contracts.Result;
using DocumentService.Data;
using DocumentService.Data.Entities;

namespace DocumentService.Application.Classes;

public class DocumentUploadService
{
    private readonly IDocumentStorageService _storage;
    private readonly DocumentDbContext _db;
    private readonly IMapper _mapper;
    private readonly IRabbitMqPublisher _publisher;

    public DocumentUploadService(
        IDocumentStorageService storage,
        DocumentDbContext db,
        IMapper mapper,
        IRabbitMqPublisher publisher)
    {
        _storage = storage;
        _db = db;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<Result<Guid>> UploadAsync(DocumentUploadDto dto, CancellationToken cancellationToken = default)
    {
        var (storedFileName, internalUrl) = await _storage.SaveAsync(dto.File, cancellationToken);

        var document = _mapper.Map<Document>(dto);
        document.Id = Guid.NewGuid();
        document.StoredFileName = storedFileName;
        document.InternalUrl = internalUrl;
        document.UploadedAt = DateTime.UtcNow;
        document.Status = DocumentStatus.Pending;

        await _db.Documents.AddAsync(document, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        var evt = new DocumentUploadedEvent(
            DocumentId: document.Id,
            CustomerId: document.CustomerId,
            DocumentType: document.DocumentType,
            StoredFileName: document.StoredFileName,
            UploadedAt: document.UploadedAt
        );

        await _publisher.PublishAsync(evt, routingKey: "document.uploaded", cancellationToken);

        return Result<Guid>.Success(document.Id, "Document uploaded and event published.");
    }
}
