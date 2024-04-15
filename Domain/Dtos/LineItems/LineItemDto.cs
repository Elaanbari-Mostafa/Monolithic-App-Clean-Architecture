using System.Diagnostics.CodeAnalysis;

namespace Domain.Dtos.LineItems;

public sealed record LineItemDto(
                        [AllowNull] Guid Id,
                        Guid ProductId,
                        decimal Price,
                        string Currency);