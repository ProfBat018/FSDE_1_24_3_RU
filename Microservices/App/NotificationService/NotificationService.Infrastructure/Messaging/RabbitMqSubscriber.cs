using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using NotificationService.Application.Interfaces;
using NotificationService.Contracts.DTOs;

namespace NotificationService.Infrastructure.Messaging;

public class RabbitMqSubscriber : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _config;
    private readonly ILogger<RabbitMqSubscriber> _logger;
    private IConnection? _connection;
    private IChannel? _channel;
    private string _exchange => _config["RabbitMQ:Exchange"] ?? "bank_exchange";

    public RabbitMqSubscriber(IServiceProvider serviceProvider, IConfiguration config, ILogger<RabbitMqSubscriber> logger)
    {
        _serviceProvider = serviceProvider;
        _config = config;
        _logger = logger;
    }

    private async Task InitializeConnectionAsync()
    {
        var factory = new ConnectionFactory
        {
            HostName = _config["RabbitMQ:Host"] ?? "localhost",
            Port = int.Parse(_config["RabbitMQ:Port"] ?? "5672"),
            UserName = _config["RabbitMQ:User"] ?? "guest",
            Password = _config["RabbitMQ:Pass"] ?? "guest"
        };

        _connection = await factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        await _channel.ExchangeDeclareAsync(_exchange, ExchangeType.Topic, durable: true);
        await _channel.QueueDeclareAsync("notification.transaction", durable: true, exclusive: false, autoDelete: false);
        await _channel.QueueBindAsync("notification.transaction", _exchange, "transaction.created");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await InitializeConnectionAsync();

        while (!stoppingToken.IsCancellationRequested)
        {
            var result = await _channel!.BasicGetAsync("notification.transaction", autoAck: true);
            if (result is not null)
            {
                var json = Encoding.UTF8.GetString(result.Body.ToArray());

                try
                {
                    var transaction = JsonSerializer.Deserialize<TransactionCreatedEvent>(json);

                    if (transaction is null) return;

                    using var scope = _serviceProvider.CreateScope();
                    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                    var message = $"Your {transaction.Type.ToLower()} of {transaction.Amount:C} has been processed.";

                    var dto = new CreateNotificationDto(
                        transaction.UserId,
                        "TransactionCreated",
                        message
                    );

                    await notificationService.CreateAsync(dto);
                    _logger.LogInformation("Processed transaction.created for user {UserId}", transaction.UserId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to process message: {Json}", json);
                }
            }

            await Task.Delay(1000, stoppingToken); 
        }
    }

    public override void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
        base.Dispose();
    }
}
