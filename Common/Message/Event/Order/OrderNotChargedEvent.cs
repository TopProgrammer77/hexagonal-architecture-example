
using Common.Marker.Message;
using System.Runtime.Serialization;

namespace Common.Message.Event.Order;

[MessageDetails(
        Publisher = Service.BILLING_SERVICE,
        Subscribers = [Service.ORDER_SERVICE],
        Channel = Channel.ORDER_NOT_CHARGED
)]
public class OrderNotChargedEvent : AbstractDomainEvent {

    private static readonly string NAME = "OrderNotCharged";

    public OrderNotChargedEvent(long messageId, long eventId, long customerId, long orderId, string reason) 
        : base(messageId, eventId)
    {
        CustomerId = customerId;
        OrderId = orderId;
        Reason = reason;
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

    public string Reason
    {
        get; private set;
    }

    public override bool Equals(object? o) {
        if (this == o) return true;
        if (o == null || GetType() != o.GetType()) return false;
        OrderNotChargedEvent that = (OrderNotChargedEvent) o;
        return CustomerId == that.CustomerId &&
                OrderId == that.OrderId &&
                Equals(Reason, that.Reason);
    }

    public override int GetHashCode() {
        return HashCode.Combine(CustomerId, OrderId, Reason);
    }
}
