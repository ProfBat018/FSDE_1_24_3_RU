namespace ImageService.Contracts.DTOs;

public record StoredImageDto(
    Guid Id,
    string ImagePath,
    string Extension,
    long SizeInBytes,
    DateTime UploadedAt
);