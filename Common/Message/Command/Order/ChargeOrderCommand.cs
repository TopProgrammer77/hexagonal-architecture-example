
using Common.Marker.Message;
using Common.Message;
using System.Runtime.Serialization;

namespace Common.Message.Command.Order;

[MessageDetails(
        Publisher = Service.ORDER_SERVICE,
        Subscribers = [ Service.BILLING_SERVICE ],
        Channel = Channel.CHARGE_ORDER
)]
public class ChargeOrderCommand : AbstractCommand
{

    private static readonly string NAME = "ChargeOrder";

    public ChargeOrderCommand(long messageId, long customerId, long orderId, double orderTotal, CurrencyType currency)
        : base(messageId)
    {
        CustomerId = customerId;
        OrderId = orderId;
        OrderTotal = orderTotal;
        Currency = currency;
    }

    public long CustomerId
    {
        get; private set;
    }

    public long OrderId
    {
        get; private set;
    }

    public double OrderTotal
    {
        get; private set;
    }

    public CurrencyType Currency
    {
        get; private set;
    }


    public override string Name
    {
        get { return NAME; }
    }

    public enum CurrencyType
    {
        EUR,
        USD
    }

    public override string ToString()
    {
        return "customerId: " + CustomerId +
                ", orderId: " + OrderId +
                ", orderTotal: " + OrderTotal +
                ", currency: " + Currency +
                '}';
    }
}
