using Common.Marker.Port;
using Common.Message.Command.Order;
using Common.Message.Event.Order;

namespace OrderService.Outbound.Port;

public interface IOutboundMessagingPort : IOutboundPort
{
    void PublishOrderCreatedEvent(OrderCreatedEvent orderCreatedEvent);

    void PublishChargeOrderCommand(ChargeOrderCommand chargeOrderCommand);
}
