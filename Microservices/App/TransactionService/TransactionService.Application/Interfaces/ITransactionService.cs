
using TransactionService.Contracts.DTOs;
using TransactionService.Contracts.Response;

namespace TransactionService.Application.Interfaces;

public interface ITransactionService
{
    Task<Result<TransactionDto>> CreateAsync(CreateTransactionDto dto);
    Task<Result<IEnumerable<TransactionDto>>> GetByUserIdAsync(string userId);
    Task<Result<string>> DeleteAsync(Guid id);
}