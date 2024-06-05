using Common.DTO.Order;
using Common.Marker.Port;

namespace BillingService.Inbound.Port;

public interface IInboundRestPort : IInboundPort
{
    string ChargeCustomer(long customerId, long orderId);

    List<PaymentDTO> GetPaymentsForCustomer(long customerId);
}

