using Domain.Dtos.LineItems;

namespace Application.Orders.UpdateOrder;

public sealed record UpdateOrderCommand(
                    Guid OrderId,
                    IList<LineItemDto> LineItems) : ICommand;