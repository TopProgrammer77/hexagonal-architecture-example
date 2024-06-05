using Common.DTO.Order;
using Common.Marker.Port;

namespace OrderService.Inbound.Port;

public interface IInboundRestPort : IInboundPort
{
    void CreateOrder(OrderDTO orderDTO);

    void ChargeOrder(long customerId, long orderId);
}
