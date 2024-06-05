

namespace Common.Message.Event.Order;

public class OrderCreatedEvent : AbstractDomainEvent {

    private static readonly string NAME = "OrderCreated";

    public OrderCreatedEvent(long messageId, long eventId, long customerId, long orderId) : base(messageId, eventId)
    {
        CustomerId = customerId;
        OrderId = orderId;
    }

    public override string Name
    {
        get { return NAME; }
    }

    public long CustomerId
    {
        get; private set;
    }

    public long OrderId
    {
        get; private set;
    }

    public override string ToString() {
        return "customerId: " + CustomerId +
                ", orderId: " + OrderId +
                '}';
    }
}