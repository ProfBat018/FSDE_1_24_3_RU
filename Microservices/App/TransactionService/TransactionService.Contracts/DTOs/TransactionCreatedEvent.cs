namespace TransactionService.Contracts.DTOs;

public record TransactionCreatedEvent(
    Guid TransactionId,
    string UserId,
    string ToUserId,
    decimal Amount,
    string Type,
    DateTime CreatedAt
);