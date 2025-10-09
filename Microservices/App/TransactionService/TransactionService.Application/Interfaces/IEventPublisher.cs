using TransactionService.Contracts.DTOs;

namespace TransactionService.Application.Interfaces;

public interface IEventPublisher
{
    Task PublishAsync(string topic, AuditEventDto message);
}