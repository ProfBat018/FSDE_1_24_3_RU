namespace NotificationService.Contracts.DTOs;

public record CreateNotificationDto(
    string UserId,
    string Type,
    string Message
);