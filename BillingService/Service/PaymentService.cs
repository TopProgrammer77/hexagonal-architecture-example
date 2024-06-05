using BillingService.Outbound.Port;
using Common.DTO.Order;

namespace BillingService.Service;

public class PaymentService : IOutboundRestPort
{
    public OrderChargingStatusDTO Charge(int paymentMethodId, double amount)
    {
        return DateTime.Now.Millisecond % 2 == 0 ? new OrderChargingStatusDTO() :
                new OrderChargingStatusDTO(false, "The card has expired");
    }
}
