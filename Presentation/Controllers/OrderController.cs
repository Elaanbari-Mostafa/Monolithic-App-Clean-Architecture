﻿using Presentation.Contracts.Orders;
using Application.Orders.CreateOrder;
using Application.Orders.UpdateOrder;

namespace Presentation.Controllers;

public sealed class OrderController : ApiController
{
    public OrderController(ISender sender) : base(sender)
    {
    }

    [HasPermission(EPermission.CreateOrder)]
    [HttpPost(Routers.Order.Create)]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var createOrderCommand = request.Adapt<CreateOrderCommand>();

        var result = await Sender.Send(createOrderCommand, cancellationToken);

        return result.MapActionResult(
            id => Ok(id),
            NotFound);
    }

    //[HasPermission(Permission.UpdateOrder)]
    [HttpPut(Routers.Order.Update)]
    public async Task<IActionResult> UpdateOrderAsync([FromBody] UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        var updateOrderCommand = request.Adapt<UpdateOrderCommand>();

        var result = await Sender.Send(updateOrderCommand, cancellationToken);

        return result.MapActionResult(NoContent, NotFound);
    }
}