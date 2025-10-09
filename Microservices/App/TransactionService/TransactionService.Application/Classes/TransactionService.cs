using System.Text.Json;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TransactionService.Application.Interfaces;
using TransactionService.Contracts.DTOs;
using TransactionService.Contracts.Response;
using TransactionService.Data;
using TransactionService.Data.Entities;

namespace TransactionService.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly TransactionDbContext _context;
    private readonly IMapper _mapper;
    private readonly IEventPublisher _eventPublisher;

    public TransactionService(TransactionDbContext context, IMapper mapper, IEventPublisher eventPublisher)
    {
        _context = context;
        _mapper = mapper;
        _eventPublisher = eventPublisher;
    }

    public async Task<Result<TransactionDto>> CreateAsync(CreateTransactionDto dto)
    {
        var transaction = _mapper.Map<Transaction>(dto);
        transaction.Id = Guid.NewGuid();

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        
        await _eventPublisher.PublishAsync("transaction.created", new AuditEventDto
        {
            EventType = "TransactionCreated",
            Source = "TransactionService",
            Target = transaction.ToUserId,
            Description = "transaction created.",
            Metadata = JsonSerializer.Serialize(transaction),
            CreatedAt = DateTime.UtcNow
        });


        var result = _mapper.Map<TransactionDto>(transaction);
        return Result<TransactionDto>.Success(result);
    }

    public async Task<Result<IEnumerable<TransactionDto>>> GetByUserIdAsync(string userId)
    {
        var transactions = await _context.Transactions
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        var result = _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        return Result<IEnumerable<TransactionDto>>.Success(result);
    }

    public async Task<Result<string>> DeleteAsync(Guid id)
    {
        var transaction = await _context.Transactions.FindAsync(id);
        if (transaction == null)
            return Result<string>.Error("Transaction not found", 404);

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        
        await _eventPublisher.PublishAsync("transaction.deleted", new AuditEventDto
        {
            EventType = "TransactionDeleted",
            Source = "TransactionService",
            Target = transaction.ToUserId,
            Description = "transaction deleted.",
            Metadata = JsonSerializer.Serialize(transaction),
            CreatedAt = DateTime.UtcNow
        });


        return Result<string>.Success("Transaction deleted");
    }
}