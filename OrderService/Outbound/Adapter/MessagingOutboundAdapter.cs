using Common.Marker.Adapter;
using Common.Message.Command.Order;
using Common.Message.Event.Order;
using Common.Message;
using OrderService.Outbound.Port;
using Common.Helper;

namespace OrderService.Outbound.Adapter;

public class MessagingOutboundAdapter : IOutboundMessagingPort, IOutboundAdapter
{
    private static readonly ILogger _logger = new LoggerFactory().CreateLogger(typeof(MessagingOutboundAdapter));

    private readonly MessagePublisher _messagePublisher;

    public MessagingOutboundAdapter(MessagePublisher messagePublisher)
    {
        _messagePublisher = messagePublisher;
    }

    public void PublishOrderCreatedEvent(OrderCreatedEvent orderCreatedEvent)
    {
        //TODO find a way to directly use the orderCreatedProducer
        _messagePublisher.SendMessage("order-created", orderCreatedEvent);
        _logger.LogInformation("The OrderCreatedEvent '{0}' was published", orderCreatedEvent);
    }

    public void PublishChargeOrderCommand(ChargeOrderCommand chargeOrderCommand)
    {
        //TODO find a way to directly use the chargeOrderProducer
        _messagePublisher.SendMessage("charge-order", chargeOrderCommand);
        _logger.LogInformation("The ChargeOrderCommand '{0}' was published", chargeOrderCommand);
    }
}
