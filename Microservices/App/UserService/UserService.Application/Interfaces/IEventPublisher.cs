using UserService.Contracts.DTOs;

namespace UserService.Application.Interfaces;

public interface IEventPublisher
{
    Task PublishAsync(string routingKey, AuditEventDto message);
}
