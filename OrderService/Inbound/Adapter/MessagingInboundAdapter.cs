
using Common.Helper;
using Common.Marker.Adapter;
using Common.Message;
using Common.Message.Command.Order;
using Confluent.Kafka;
using OrderService.Inbound.Port;
using System.Text.Json;

namespace OrderService.Inbound.Adapter
{
    public class MessagingInboundAdapter : BackgroundService, IInboundAdapter 
    {
        private static readonly ILogger<MessagingInboundAdapter> Logger = new LoggerFactory().CreateLogger<MessagingInboundAdapter>();

        private IInboundMessagingPort? _inboundMessagingPort;
        private readonly IConsumer<Null, string> _consumer;
        private readonly IServiceProvider _serviceProvider;

        public MessagingInboundAdapter(IServiceProvider serviceProvider, IConfiguration configuration, ILogger<MessagingInboundAdapter> logger)
        {

            var config = new ConsumerConfig
            {
                BootstrapServers = configuration.GetConnectionString("Kafka:BootstrapServers") ?? "localhost:9092",
                GroupId = "order-service-group",
                AllowAutoCreateTopics = true,
            };
            _consumer = new ConsumerBuilder<Null, string>(config).Build();
            _serviceProvider = serviceProvider;
        }

        public Action<CreateOrderCommand> CreateOrder()
        {
            return createOrderCommand =>
            {
                Console.WriteLine("--------------------------------------------------------------------------------------------------------------");
                Logger.LogDebug("Received a '{0}' command, the ordered item is '{1}', the customer ID is {2}",
                    createOrderCommand.Name, createOrderCommand.CustomerId, createOrderCommand.CustomerId);
                
                _inboundMessagingPort?.CreateOrder(createOrderCommand);
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _inboundMessagingPort = _serviceProvider.GetRequiredService<IInboundMessagingPort>();
            _consumer.Subscribe(new List<string> { "create-order", "order-charged", "order-not-charged" });

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                var consumeResult = _consumer.Consume(stoppingToken);
                switch (consumeResult.Topic)
                {
                    case "create-order":
                        var createOrderCommand = JsonSerializer.Deserialize<CreateOrderCommand>(consumeResult.Message.Value);
                        if (createOrderCommand != null)
                        {
                            CreateOrder().Invoke(createOrderCommand);
                        }
                        break;
                    case "order-charged":
                        Logger.LogInformation("Order charged successfully!");
                        break;
                    case "order-not-charged":
                        Logger.LogInformation("Order not charged!");
                        break;

                }
            }

            _consumer.Close();
        }
    }
}