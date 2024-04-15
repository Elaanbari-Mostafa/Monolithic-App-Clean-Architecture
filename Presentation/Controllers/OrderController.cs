using Application.Orders.CreateOrder;
using Application.Orders.UpdateOrder;
using Domain.Enums;
using Domain.Shared;
using Infrastructure.Authentification;
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

    [HasPermission(Permission.CreateOrder)]
    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var createOrderCommand = request.Adapt<CreateOrderCommand>();

        var result = await Sender.Send(createOrderCommand, cancellationToken);

        return result.MapActionResult(
            id => Ok(id),
            NotFound);
    }

    //[HasPermission(Permission.UpdateOrder)]
    [HttpPut]
    public async Task<IActionResult> UpdateOrderAsync([FromBody] UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        var updateOrderCommand = request.Adapt<UpdateOrderCommand>();

        var result = await Sender.Send(updateOrderCommand, cancellationToken);

        return result.MapActionResult(NoContent, NotFound);
    }
}