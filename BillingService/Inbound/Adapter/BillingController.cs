using BillingService.Inbound.Port;
using Common.DTO.Order;
using Common.Marker.Adapter;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BillingService.Inbound.Adapter;

[Route("billing")]
[Controller]
public class BillingController : IInboundAdapter
{
    private readonly IInboundRestPort inboundRestPort;

    public BillingController(IInboundRestPort inboundRestPort)
    {
        this.inboundRestPort = inboundRestPort;
    }

    [HttpPost("{customerId:long}/{orderId:long}")]
    public ActionResult ChargeCustomer(long customerId, long orderId)
    {
        string result = inboundRestPort.ChargeCustomer(customerId, orderId);
        return result.Contains("not") ? new BadRequestObjectResult(result) : new OkObjectResult(result);
    }

    [HttpGet("{customerId:long}")]
    public List<PaymentDTO> GetPaymentsForCustomer(long customerId)
    {
        return inboundRestPort.GetPaymentsForCustomer(customerId);
    }
}

