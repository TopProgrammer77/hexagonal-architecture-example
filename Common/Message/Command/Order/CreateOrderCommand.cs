using Common.Marker.Message;

namespace Common.Message.Command.Order;

[MessageDetails(
        Publisher = Service.ORDER_SERVICE,
        Subscribers = [Service.ORDER_SERVICE],
        Channel = Channel.CREATE_ORDER
)]
public class CreateOrderCommand : AbstractCommand
{
    private static readonly string NAME = "CreateOrder";

    public CreateOrderCommand(long customerId, long messageId, string productName, double orderTotal) : base(messageId)
    {
        CustomerId = customerId;
        ProductName = productName;
        OrderTotal = orderTotal;
    }

    public long CustomerId
    {
        get; private set;
    }

    public string ProductName
    {
        get; private set;
    }

    public double OrderTotal
    {
        get; private set;
    }

    public override string Name
    {
        get { return NAME; }
    }

    public override bool Equals(object? o)
    {
        if (this == o)
            return true;
        if (o == null || GetType() != o.GetType())
            return false;
        CreateOrderCommand that = (CreateOrderCommand)o;
        return OrderTotal == OrderTotal &&
                CustomerId == that.CustomerId &&
        Equals(ProductName, that.ProductName);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ProductName, OrderTotal, CustomerId);
    }
}