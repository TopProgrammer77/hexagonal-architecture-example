using Common.DTO.Order;
using Common.Marker.Port;

namespace BillingService.Outbound.Port;

public interface IOutboundRestPort : IOutboundPort
{
    OrderChargingStatusDTO Charge(int paymentMethodId, double amount);
}
