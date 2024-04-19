using Domain.Dtos.LineItems;
using Domain.ValueObjects;

namespace Domain.Dtos.Orders;

public sealed record class OrderWithProductDto(
                                    Guid OrderId,
                                    DateTime CreatedOnUtc,
                                    Money OrderPrice,
                                    IEnumerable<LineItemDetailDto> LineItems);