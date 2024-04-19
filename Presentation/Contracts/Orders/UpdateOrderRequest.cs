using Domain.Dtos.LineItems;

namespace Presentation.Contracts.Orders;

public sealed record UpdateOrderRequest(
                          Guid OrderId,
                          IList<LineItemDto> LineItems);