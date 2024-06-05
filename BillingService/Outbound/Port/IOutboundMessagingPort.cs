using Common.Message.Event.Order;
using Common.Marker.Port;

namespace BillingService.Outbound.Port;

public interface IOutboundMessagingPort : IOutboundPort
{
    void PublishOrderChargedEvent(OrderChargedEvent orderChargedEvent);

    void PublishOrderNotChargedEvent(OrderNotChargedEvent orderNotChargedEvent);
}
