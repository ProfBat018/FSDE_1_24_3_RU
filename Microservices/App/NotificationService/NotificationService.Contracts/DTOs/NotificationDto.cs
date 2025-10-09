namespace NotificationService.Contracts.DTOs;

public record NotificationDto(
    Guid Id,
    string UserId,
    string Type,
    string Message,
    bool IsRead,
    DateTime CreatedAt
);