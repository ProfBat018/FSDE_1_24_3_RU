using Microsoft.AspNetCore.Http;

namespace ImageService.Contracts.DTOs;

public record UploadImageDto(
    IFormFile File
);