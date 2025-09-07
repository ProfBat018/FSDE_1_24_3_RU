using ContactService.Contracts.DTOs;

namespace ContactService.Application.Interfaces;

public interface IEventPublisher
{
    Task PublishAsync(string routingKey, AuditEventDto message);
}
