using Common.Marker.Port;
using OrderService.Domain.Model;


namespace OrderService.Outbound.Port;

public interface IOutboundPersistencePort : IOutboundPort
{
    long Save(Order order);
}
