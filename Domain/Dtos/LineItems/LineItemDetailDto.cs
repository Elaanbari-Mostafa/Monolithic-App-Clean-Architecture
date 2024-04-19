using Domain.ValueObjects;

namespace Domain.Dtos.LineItems;

public sealed record LineItemDetailDto(
                                    Guid LineItemId,
                                    string ProductName,
                                    string Brand,
                                    int Qty,
                                    Money ProductPrice);