using System.Text;
using System.Text.Json;
using AuditService.Application.Interfaces;
using AuditService.Contracts.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace AuditService.Infrastructure.Messaging;

public class RabbitMqSubscriber : BackgroundService
{
    private readonly ILogger<RabbitMqSubscriber> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _config;
    private IConnection? _connection;
    private IChannel? _channel;

    private string ExchangeName => _config["RabbitMQ:Exchange"] ?? "bank_exchange";
    private string QueueName => _config["RabbitMQ:Queue"] ?? "audit_queue";
    private string Host => _config["RabbitMQ:Host"] ?? "localhost";

    public RabbitMqSubscriber(
        ILogger<RabbitMqSubscriber> logger,
        IServiceProvider serviceProvider,
        IConfiguration config)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _config = config;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = Host
        };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Topic, durable: true);
        await _channel.QueueDeclareAsync(QueueName, durable: true, exclusive: false, autoDelete: false);
        await _channel.QueueBindAsync(QueueName, ExchangeName, routingKey: "#");

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += HandleMessageAsync;

        await _channel.BasicConsumeAsync(queue: QueueName, autoAck: true, consumer: consumer);

        _logger.LogInformation("RabbitMQ subscriber is listening on queue '{QueueName}'", QueueName);
    }

    private async Task HandleMessageAsync(object sender, BasicDeliverEventArgs args)
    {
        try
        {
            var json = Encoding.UTF8.GetString(args.Body.ToArray());

            var dto = JsonSerializer.Deserialize<AuditEventDto>(json);
            if (dto is null)
            {
                _logger.LogWarning("Received invalid or null AuditEventDto. Raw: {Json}", json);
                return;
            }

            using var scope = _serviceProvider.CreateScope();
            var repo = scope.ServiceProvider.GetRequiredService<IAuditLogRepository>();
            await repo.SaveAsync(dto);

            _logger.LogInformation("Audit event processed: {EventType} → {Target}", dto.EventType, dto.Target);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing audit message");
        }
    }

    public override void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
        base.Dispose();
    }
}
