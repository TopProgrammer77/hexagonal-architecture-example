using OrderService.Domain.Model;
using OrderService.Domain.Repository;
using OrderService.Outbound.Port;
using System.Text.Json;

namespace OrderService.Outbound.Adapter;

public class OutboundPersistenceAdapter : IOutboundPersistencePort
{
    private readonly OrderRepository orderRepository;

    public OutboundPersistenceAdapter(OrderRepository orderRepository)
    {
        this.orderRepository = orderRepository;
    }

    public long Save(Order order)
    {
        Console.WriteLine($"Order: {JsonSerializer.Serialize(order)}");
        orderRepository.Orders.Add(order);
        orderRepository.SaveChanges();
        return order.Id;
    }
}
