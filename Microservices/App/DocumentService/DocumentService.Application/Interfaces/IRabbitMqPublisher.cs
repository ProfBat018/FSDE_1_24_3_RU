namespace DocumentService.Application.Interfaces;

public interface IRabbitMqPublisher
{
    Task PublishAsync<T>(T message, string routingKey, CancellationToken cancellationToken = default);
}