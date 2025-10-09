using Microsoft.AspNetCore.Http;

namespace DocumentService.Application.Interfaces;

public interface IDocumentStorageService
{
  
    Task<(string StoredFileName, string InternalUrl)> SaveAsync(IFormFile file, CancellationToken cancellationToken = default);
}