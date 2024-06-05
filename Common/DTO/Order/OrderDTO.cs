
namespace Common.DTO.Order;

public class OrderDTO : AbstractDTO
{
    public long CustomerId
    {
        get; set;
    }

    public long OrderId
    {
        get; set;
    }

    public List<OrderItemDTO> OrderItems
    {
        get; set;
    } = new List<OrderItemDTO>();

    public double GetOrderTotal()
    {
        return OrderItems.Select(item => item.ProductPrice).Sum();
    }

    public override bool Equals(object? obj)
    {
        if (this == obj)
            return true;
        if (obj == null || GetType() != obj.GetType())
            return false;
        OrderDTO other = (OrderDTO)obj;
        return CustomerId == other.CustomerId &&
               OrderId == other.OrderId &&
               Equals(OrderItems, other.OrderItems);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(CustomerId, OrderId, OrderItems);
    }
}
