namespace NotificationService.Application.Interfaces;

public interface IEventSubscriber
{
    Task StartAsync(CancellationToken cancellationToken);
}