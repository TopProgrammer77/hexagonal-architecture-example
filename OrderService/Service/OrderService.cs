using Common.DTO.Order;
using Common.Message.Command.Order;
using Common.Message.Event.Order;
using OrderService.Domain.Model;
using OrderService.Inbound.Port;
using OrderService.Outbound.Port;
using RestSharp;

namespace OrderService.Service;

public class OrderService : IInboundRestPort, IInboundMessagingPort
{
    private readonly ILogger<OrderService> LOGGER;
    private static readonly Random RANDOM = new Random(100);

    private readonly IOutboundMessagingPort outboundMessagingPort;
    private readonly IOutboundPersistencePort outboundPersistencePort;

    private string? billingServiceEndpoint;

    public OrderService(IConfiguration _configuration, ILogger<OrderService> logger, IOutboundMessagingPort outboundMessagingPort, IOutboundPersistencePort outboundPersistencePort)
    {
        this.outboundMessagingPort = outboundMessagingPort;
        this.outboundPersistencePort = outboundPersistencePort;
        LOGGER = logger;
        billingServiceEndpoint = _configuration["BillingService:Endpoint"];
    }

    // creating an order received from a REST endpoint (UI, testing app etc)
    //[Transaction]
    public void CreateOrder(OrderDTO orderDTO)
    {
        long customerId = orderDTO.CustomerId;
        long orderId = SaveOrder(ConvertDTOIntoOrder(orderDTO));

        LOGGER.LogInformation("Creating an order for the customer with the ID {0}, for {1} items...", customerId, orderDTO.OrderItems.Count);
        double orderTotal = orderDTO.GetOrderTotal();

        Task.Run(() => PublishChargeOrder(customerId, orderId, orderTotal))
            .ContinueWith(_ => PublishOrderCreatedEvent(customerId, orderId));
    }

    public void ChargeOrder(long customerId, long orderId)
    {
        RestClient restClient = new RestClient();
        RestResponse<string> response = ChargeOrder(customerId, orderId, restClient);
        Console.WriteLine("Got the response: {0}", response.Content);
        LOGGER.LogInformation("Got the response: {0}", response.Content);
    }

    private RestResponse<string> ChargeOrder(long customerId, long orderId, RestClient restClient)
    {
        string url = billingServiceEndpoint + "/" + customerId + "/" + orderId;
        var request = new RestRequest(url, Method.Post);
        Console.WriteLine($"billingServiceURL: {url}");
        return restClient.Execute<string>(request);
    }

    // creating an order received from a messaging endpoint (upstream system, 3rd party application etc)
    //[Transaction]
    public void CreateOrder(CreateOrderCommand createOrderCommand)
    {
        long customerId = createOrderCommand.CustomerId;
        LOGGER.LogInformation("Creating an order for the product '{0}', for the customer with the ID {1}...",
            createOrderCommand.ProductName, customerId);

        long orderId = SaveOrder(ConvertCommandIntoOrder(createOrderCommand));
        double orderTotal = createOrderCommand.OrderTotal;

        PublishChargeOrder(customerId, orderId, orderTotal);
        PublishOrderCreatedEvent(customerId, orderId);
    }

    private void PublishChargeOrder(long customerId, long orderId, double orderTotal)
    {
        outboundMessagingPort.PublishChargeOrderCommand(
            new ChargeOrderCommand(GetNextMessageId(), customerId, orderId, orderTotal,
                    ChargeOrderCommand.CurrencyType.EUR));
    }

    private void PublishOrderCreatedEvent(long customerId, long orderId)
    {
        outboundMessagingPort.PublishOrderCreatedEvent(
            new OrderCreatedEvent(GetNextMessageId(), GetNextEventId(), customerId, orderId));
    }

    private Order ConvertCommandIntoOrder(CreateOrderCommand command)
    {
        return new Order(command.CustomerId, command.OrderTotal);
    }

    private Order ConvertDTOIntoOrder(OrderDTO orderDTO)
    {
        return new Order(orderDTO.CustomerId, orderDTO.GetOrderTotal());
    }

    private long SaveOrder(Order order)
    {
        return outboundPersistencePort.Save(order);
    }

    private long GetNextMessageId()
    {
        return RANDOM.Next();
    }

    private long GetNextEventId()
    {
        // returned from the saved database event, before sending it (using transactional messaging)
        return RANDOM.Next();
    }
}
