using Common.Marker.Port;
using Common.Message.Command.Order;

namespace OrderService.Inbound.Port;

public interface IInboundMessagingPort : IInboundPort
{
    void CreateOrder(CreateOrderCommand createOrderCommand);
}
