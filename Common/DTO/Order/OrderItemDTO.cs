using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO.Order;

public class OrderItemDTO : AbstractDTO
{
    public OrderItemDTO(long orderItemId, string productName, double productPrice)
    {
        OrderItemId = orderItemId;
        ProductName = productName;
        ProductPrice = productPrice;
    }

    public long OrderItemId
    {
        get; private set;
    }

    public string ProductName
    {
        get; private set;
    }

    public double ProductPrice
    {
        get; private set;
    }

    public override bool Equals(object? obj)
    {
        if (this == obj)
            return true;
        if (obj == null || GetType() != obj.GetType())
            return false;
        OrderItemDTO that = (OrderItemDTO)obj;
        return OrderItemId == that.OrderItemId &&
               ProductPrice == that.ProductPrice &&
               string.Equals(ProductName, that.ProductName);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(OrderItemId, ProductName, ProductPrice);
    }
}


