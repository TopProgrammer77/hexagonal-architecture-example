using Common.Marker.Port;
using Common.Message.Command.Order;

namespace BillingService.Inbound.Port;

public interface IInboundMessagingPort : IInboundPort
{
    void ChargeOrder(ChargeOrderCommand chargeOrderCommand);
}

