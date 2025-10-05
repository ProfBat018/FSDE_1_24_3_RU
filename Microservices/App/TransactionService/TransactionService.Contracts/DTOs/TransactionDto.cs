
using TransactionService.Data.Entities;

namespace TransactionService.Contracts.DTOs;

public record TransactionDto(
    Guid Id,
    string UserId,
    string ToUserId,
    decimal Amount,
    TransactionType Type,
    string? Description,
    DateTime CreatedAt
);