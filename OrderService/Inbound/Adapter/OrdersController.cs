using Common.DTO.Order;
using Common.Marker.Adapter;
using Microsoft.AspNetCore.Mvc;
using OrderService.Inbound.Port;

namespace OrderService.Inbound.Adapter;

[Controller]
[Route("order")]
public class OrdersController : IInboundAdapter
{
    private readonly IInboundRestPort inboundRestPort;

    public OrdersController(IInboundRestPort inboundRestPort)
    {
        this.inboundRestPort = inboundRestPort;
    }

    [HttpPost]
    public ActionResult<string> CreateOrder([FromBody] OrderDTO orderDTO)
    {
        inboundRestPort.CreateOrder(orderDTO);

        return new OkObjectResult("The order was successfully created");
    }

    [HttpGet("{customerId:long}/{orderId:long}")]
    public ActionResult<string> ChargeOrder(long customerId, long orderId)
    {
        inboundRestPort.ChargeOrder(customerId, orderId);

        return new OkObjectResult("The order request was successfully performed");
    }
}
