using Domain.ValueObjects;

namespace Domain.Dtos.Products;

public sealed record ProductIdPriceDto()
{
    public Guid Id { get; set; }
    public Money Money { get; set; }
}