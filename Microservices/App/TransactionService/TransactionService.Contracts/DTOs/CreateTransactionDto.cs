using TransactionService.Data.Entities;

namespace TransactionService.Contracts.DTOs;

public record CreateTransactionDto(
    string UserId,
    string ToUserId,
    decimal Amount,
    TransactionType Type,
    string? Description
);