
namespace TransactionService.Data.Entities;

public class Transaction : BaseEntity
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = default!;
    public string ToUserId { get; set; } = default!;
    public decimal Amount { get; set; }

    public TransactionType Type { get; set; }

    public string? Description { get; set; }
}