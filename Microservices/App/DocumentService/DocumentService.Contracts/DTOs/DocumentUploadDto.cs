using Microsoft.AspNetCore.Http;

namespace DocumentService.Contracts.DTOs;

public record DocumentUploadDto(
    Guid CustomerId,
    string DocumentType,
    IFormFile File
);