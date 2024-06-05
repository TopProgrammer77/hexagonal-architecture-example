using BillingService.Inbound.Port;
using Common.Marker.Adapter;
using Common.Message;
using Common.Message.Command.Order;
using Confluent.Kafka;
using System.Text;
using System.Text.Json;

namespace BillingService.Inbound.Adapter;

public class MessageConsumer : BackgroundService, IInboundAdapter
{
    private ILogger LOGGER;

    private IInboundMessagingPort? inboundMessagingPort;
    private IConsumer<Null, string> _consumer;
    private readonly IServiceProvider _serviceProvider;

    public MessageConsumer(IServiceProvider serviceProvider, IConfiguration configuration, ILogger<MessageConsumer> logger)
    {
        LOGGER = logger;
        _serviceProvider = serviceProvider;
        var config = new ConsumerConfig
        {
            BootstrapServers = configuration.GetConnectionString("Kafka:BootstrapServers") ?? "localhost:9092",
            GroupId = "billing-service-group"
        };
        _consumer = new ConsumerBuilder<Null, string>(config).Build();
    }

    public Action<ChargeOrderCommand> ChargeOrder()
    {
        return chargeOrderCommand => {
            LOGGER.LogInformation("Received a '{}' command for the order with the ID {} of the customer with the ID {}",
            chargeOrderCommand.Name, chargeOrderCommand.OrderId, chargeOrderCommand.CustomerId);

            inboundMessagingPort?.ChargeOrder(chargeOrderCommand);
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        inboundMessagingPort = _serviceProvider.GetRequiredService<IInboundMessagingPort>();
        _consumer.Subscribe("charge-order");
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            var consumeResult = _consumer.Consume(stoppingToken);
            var message = consumeResult.Message.Value;
            LOGGER.LogInformation($"message: {message}");
            ChargeOrderCommand? command = JsonSerializer.Deserialize<ChargeOrderCommand>(message);
            if (command != null)
            {
                ChargeOrder().Invoke(command);
            }
        }
        _consumer.Close();
    }
}
