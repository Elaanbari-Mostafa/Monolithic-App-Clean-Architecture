using Application.Orders.CreateOrder;
using Domain.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Contracts.Orders;

namespace Presentation.Controllers;

[Route("api/[controller]")]
public sealed class OrderController : ApiController
{
    public OrderController(ISender sender) : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var createOrderCommand = request.Adapt<CreateOrderCommand>();

        var result = await Sender.Send(createOrderCommand, cancellationToken);

        return result.MapActionResult(
            id => Ok(id),
            NotFound);
    }
}
