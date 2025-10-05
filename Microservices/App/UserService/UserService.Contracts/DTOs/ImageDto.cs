namespace UserService.Contracts.DTOs;


public record ImageDto(
    Guid Id,
    string ImagePath,
    string Extension,
    long SizeInBytes,
    DateTime UploadedAt
);