using Application.Abstractions.Messaging;
using Domain.Dtos.LineItems;
using Domain.Entities;

namespace Application.Orders.UpdateOrder;

public sealed record UpdateOrderCommand(
                    Guid OrderId,
                    IList<LineItemDto> LineItems) : ICommand;