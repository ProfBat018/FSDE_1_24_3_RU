using ImageService.Contracts.DTOs;
using ImageService.Contracts.Response;
using Microsoft.AspNetCore.Http;

namespace ImageService.Application.Interfaces;

public interface IStoredImageService
{
    Task<Result<StoredImageDto>> UploadAsync(IFormFile file);
    Task<Result<StoredImageDto>> GetByIdAsync(Guid id);
    Task<Result<string>> DeleteAsync(Guid id);
}