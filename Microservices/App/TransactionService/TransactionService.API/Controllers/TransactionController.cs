using Microsoft.AspNetCore.Mvc;
using TransactionService.Application.Interfaces;
using TransactionService.Contracts.DTOs;
using TransactionService.Contracts.Response;

namespace TransactionService.API.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    public async Task<ActionResult<Result<TransactionDto>>> Create(CreateTransactionDto dto)
    {
        var result = await _transactionService.CreateAsync(dto);
        return StatusCode(result.IsSuccess ? 201 : result.ErrorCode ?? 400, result);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<Result<IEnumerable<TransactionDto>>>> GetByUser(string userId)
    {
        var result = await _transactionService.GetByUserIdAsync(userId);
        return StatusCode(result.IsSuccess ? 200 : result.ErrorCode ?? 400, result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<string>>> Delete(Guid id)
    {
        var result = await _transactionService.DeleteAsync(id);
        return StatusCode(result.IsSuccess ? 200 : result.ErrorCode ?? 400, result);
    }
}