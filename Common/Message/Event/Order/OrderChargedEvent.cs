using Common.Marker.Message;

namespace Common.Message.Event.Order;

[MessageDetails(
        Publisher = Service.BILLING_SERVICE,
        Subscribers = [
                Service.BILLING_SERVICE,
Service.ORDER_SERVICE
        ],
        Channel = Channel.ORDER_CHARGED
)]
public class OrderChargedEvent : AbstractDomainEvent
{
    private static readonly string NAME = "OrderCharged";

    public OrderChargedEvent(long messageId, long eventId, long customerId, long orderId)
        : base(messageId, eventId)
    {
        CustomerId = customerId;
        OrderId = orderId;
    }

    public override string Name 
    {
        get { return NAME; }
    }

    public long CustomerId {
        get; private set;
    }

    public long OrderId {
        get; private set;
    }

    public override bool Equals(object? o) {
        if (this == o) return true;
        if (o == null || GetType() != o.GetType()) return false;
        OrderChargedEvent that = (OrderChargedEvent) o;
        return CustomerId == that.CustomerId &&
                OrderId == that.OrderId;
    }

    public override int GetHashCode() {
        return HashCode.Combine(CustomerId, OrderId);
    }
}
