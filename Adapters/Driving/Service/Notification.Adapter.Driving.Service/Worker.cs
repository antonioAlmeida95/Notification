using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Notification.Adapter.Driving.Service.Settings;
using Notification.Core.Domain.Model;
using Notification.Core.Domain.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Notification.Adapter.Driving.Service;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly QueueSettings _queueSettings;
    private readonly CredentialsServer _credentialsServer;
    private readonly INotificacaoWorkerService _notificacaoWorkerService;
    
    public Worker(ILogger<Worker> logger,
        IOptions<QueueSettings> queueSettings,
        IOptions<CredentialsServer> credentialServer,
        INotificacaoWorkerService notificacaoWorkerService)
    {
        _logger = logger;
        _queueSettings = queueSettings.Value;
        _credentialsServer = credentialServer.Value;
        _notificacaoWorkerService = notificacaoWorkerService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = CriarConnectionFactory();
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        channel.QueueDeclare(queue: _queueSettings.Name,
            durable: _queueSettings.Durable,
            exclusive: _queueSettings.Exclusive,
            autoDelete: _queueSettings.AutoDelete,
            arguments: null);
        
        while (!stoppingToken.IsCancellationRequested)
        {
            Envelope? envelope = null;
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (_, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var jsonOptions = CriarJsonOptions();
                envelope = JsonSerializer.Deserialize<Envelope>(message, jsonOptions);
             
                await _notificacaoWorkerService.ProcessarMensagemAsync(envelope?.Message);
            };
            
            channel.BasicConsume(queue: _queueSettings.Name, autoAck: true, consumer: consumer);
            
            await Task.Delay(2000, stoppingToken);
        }
    }

    private static JsonSerializerOptions CriarJsonOptions() => new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private ConnectionFactory CriarConnectionFactory() => new()
    {
        HostName = _credentialsServer.HostName,
        Password = _credentialsServer.Password,
        UserName = _credentialsServer.Password,
        AutomaticRecoveryEnabled = _credentialsServer.AutomaticRecoveryEnabled
    };
}