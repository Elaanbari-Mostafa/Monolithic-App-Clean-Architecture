using System.Diagnostics.CodeAnalysis;

namespace Domain.Dtos.LineItems;

public sealed record LineItemDto(
                        [AllowNull] Guid Id,
                        Guid ProductId,
                        int Qty,
                        decimal Price,
                        string Currency);