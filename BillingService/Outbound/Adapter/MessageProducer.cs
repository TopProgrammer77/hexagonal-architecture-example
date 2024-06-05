using Common.Message.Event.Order;
using Common.Message;
using BillingService.Outbound.Port;
using Common.Marker.Adapter;
using Common.Helper;

namespace BillingService.Outbound.Adapter;

public class MessageProducer : IOutboundMessagingPort, IOutboundAdapter {

    private readonly MessagePublisher messagePublisher;

    public MessageProducer(MessagePublisher messagePublisher)
    {
        this.messagePublisher = messagePublisher;
    }

    public void PublishOrderChargedEvent(OrderChargedEvent orderChargedEvent)
    {
        messagePublisher.SendMessage("order-charged", orderChargedEvent);
    }

    public void PublishOrderNotChargedEvent(OrderNotChargedEvent orderNotChargedEvent)
    {
        messagePublisher.SendMessage("order-not-charged", orderNotChargedEvent);
    }
}