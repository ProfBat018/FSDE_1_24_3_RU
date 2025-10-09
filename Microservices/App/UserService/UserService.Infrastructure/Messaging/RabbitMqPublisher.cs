using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using UserService.Application.Interfaces;
using UserService.Contracts.DTOs;

namespace UserService.Infrastructure.Messaging;

public class RabbitMqPublisher : IEventPublisher, IAsyncDisposable
{
    private readonly IConfiguration _config;
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly SemaphoreSlim _semaphore = new(1, 1);
    private bool _initialized = false;
    private string Exchange => _config["RabbitMQ:Exchange"] ?? "bank_exchange";

    public RabbitMqPublisher(IConfiguration config)
    {
        _config = config;
    }

    private async Task EnsureInitializedAsync()
    {
        if (_initialized) return;

        await _semaphore.WaitAsync();
        try
        {
            if (_initialized) return;

            var factory = new ConnectionFactory
            {
                HostName = _config["RabbitMQ:Host"],
                Port = int.Parse(_config["RabbitMQ:Port"] ?? "5672"),
                UserName = _config["RabbitMQ:User"],
                Password = _config["RabbitMQ:Pass"]
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(Exchange, ExchangeType.Topic, durable: true);
            _initialized = true;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task PublishAsync(string routingKey, AuditEventDto message)
    {
        await EnsureInitializedAsync();

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await _channel!.BasicPublishAsync(
            exchange: Exchange,
            routingKey: routingKey,
            mandatory: false,
            body: body
        );
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel is not null)
            await _channel.DisposeAsync();
        if (_connection is not null)
            await _connection.DisposeAsync();
    }
}
