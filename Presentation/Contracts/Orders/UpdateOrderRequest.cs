using Domain.Dtos.LineItems;
using Domain.Entities;

namespace Presentation.Contracts.Orders;

public sealed record UpdateOrderRequest( 
                        Guid OrderId,
                        IList<LineItemDto> LineItems);