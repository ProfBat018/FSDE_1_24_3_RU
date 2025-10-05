using System.Text;
using System.Text.Json;
using DocumentService.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace DocumentService.Infrastructure.Messaging;

public class RabbitMqPublisher : IRabbitMqPublisher, IAsyncDisposable
{
    private readonly IConnection _connection;
    private readonly IChannel _channel;
    private readonly string _exchange;
    private readonly ILogger<RabbitMqPublisher> _logger;

    public RabbitMqPublisher(IConfiguration config, ILogger<RabbitMqPublisher> logger)
    {
        _logger = logger;
        _exchange = config["RabbitMQ:Exchange"] ?? "bank_exchange";

        var factory = new ConnectionFactory
        {
            HostName = config["RabbitMQ:Host"] ?? "localhost"
        };

        _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult(); 
        _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

        _channel.ExchangeDeclareAsync(
            exchange: _exchange,
            type: ExchangeType.Topic,
            durable: true
        ).GetAwaiter().GetResult();
    }

    public async Task PublishAsync<T>(T message, string routingKey, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await _channel.BasicPublishAsync(
            exchange: _exchange,
            routingKey: routingKey,
            body: body,
            cancellationToken: cancellationToken
        );

        _logger.LogInformation("Published message to RabbitMQ: {RoutingKey}", routingKey);
    }

    public async ValueTask DisposeAsync()
    {
        await _channel.DisposeAsync();
        await _connection.DisposeAsync();
    }
}