using Confluent.Kafka;
using Common.Message.Command.Order;
using Common.Message;

namespace OrderService.Service;

public class OrdersClient
{
    private static long customerId = 243;
    private static long orderAmount = 200;

    private readonly IProducer<Null, AbstractMessage> _producer;

    public OrdersClient(IConfiguration configuration)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = configuration.GetConnectionString("Kafka:BootstrapServers") ?? "localhost:9092"
        };
        _producer = new ProducerBuilder<Null, AbstractMessage>(config).Build();
    }

    public Func<CreateOrderCommand> OrderProducer()
    {
        long nextCustomerId = customerId + 5;
        long nextOrderAmount = orderAmount + 125;

        return () => new CreateOrderCommand(nextCustomerId, 8234L, "An useful tablet", nextOrderAmount);
    }

    public void PublishOrderCreationMessage()
    {
        _producer.Produce("orderProducer-out-0", new Message<Null, AbstractMessage> { Value = OrderProducer().Invoke() });
    }
}
