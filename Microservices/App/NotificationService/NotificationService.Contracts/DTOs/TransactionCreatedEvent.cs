namespace NotificationService.Contracts.DTOs;

public record TransactionCreatedEvent(
    Guid TransactionId,
    string UserId,
    decimal Amount,
    string Type,
    DateTime CreatedAt
);