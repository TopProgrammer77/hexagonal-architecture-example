using BillingService.Inbound.Port;
using BillingService.Outbound.Port;
using Common.DTO.Order;
using Common.Message.Command.Order;
using Common.Message.Event.Order;

namespace BillingService.Service;

public class BillingService : IInboundRestPort, IInboundMessagingPort
{
    private readonly ILogger LOGGER;
    private static readonly Random RANDOM = new Random(100);

    private readonly IOutboundMessagingPort outboundMessagingPort;
    private readonly IOutboundRestPort outboundRestPort;

    public BillingService(ILogger<BillingService> logger, IOutboundMessagingPort outboundMessagingPort, IOutboundRestPort outboundRestPort)
    {
        this.outboundMessagingPort = outboundMessagingPort;
        this.outboundRestPort = outboundRestPort;
        LOGGER = logger;
    }

    public void ChargeOrder(ChargeOrderCommand chargeOrderCommand)
    {
        long customerId = chargeOrderCommand.CustomerId;
        long orderId = chargeOrderCommand.OrderId;
        double orderTotal = chargeOrderCommand.OrderTotal;

        LOGGER.LogInformation("Charging the customer with the ID {0}, for the order with Id {1}, for {2} {3}...", customerId, orderId,
                orderTotal, chargeOrderCommand.Currency);

        int usedPaymentMethod = GetPaymentMethod();
        OrderChargingStatusDTO orderChargingStatus = outboundRestPort.Charge(usedPaymentMethod, orderTotal);

        if (orderChargingStatus.IsSuccessful())
        {
            LOGGER.LogInformation("The customer {0} was successfully charged for the order {1}", customerId, orderId);
            outboundMessagingPort.PublishOrderChargedEvent(CreateOrderChargedEvent(customerId, orderId));
        }
        else
        {
            string failureReason = orderChargingStatus.GetFailureReason() ?? "Cannot charge the card";
            LOGGER.LogWarning("The customer {0} could not be charged for the order {1} - '{2}'", customerId, orderId, failureReason);
            outboundMessagingPort.PublishOrderNotChargedEvent(CreateOrderNotChargedEvent(customerId, orderId, failureReason));
        }
    }

    private OrderChargedEvent CreateOrderChargedEvent(long customerId, long orderId)
    {
        return new OrderChargedEvent(GetNextMessageId(), GetNextEventId(), customerId, orderId);
    }

    private OrderNotChargedEvent CreateOrderNotChargedEvent(long customerId, long orderId, string failureReason)
    {
        return new OrderNotChargedEvent(GetNextMessageId(), GetNextEventId(), customerId, orderId, failureReason);
    }

    public string ChargeCustomer(long customerId, long orderId)
    {
        LOGGER.LogInformation($"Customer was charged.[customerId={customerId}, orderId={orderId}]");
        return DateTime.Now.Ticks % 2 == 0 ? "The customer was charged" : "The charging has failed";
    }

    public List<PaymentDTO> GetPaymentsForCustomer(long customerId)
    {
        LOGGER.LogInformation($"GePaymentsForCustomer was called.[customerId={customerId}]");
        return new List<PaymentDTO>();
    }

    private int GetPaymentMethod()
    {
        // return the user's payment methods
        return RANDOM.Next(20);
    }

    private long GetNextMessageId()
    {
        return RANDOM.Next();
    }

    private long GetNextEventId()
    {
        // returned from the saved database event, before sending it (using transactional messaging)
        return RANDOM.Next();
    }
}
