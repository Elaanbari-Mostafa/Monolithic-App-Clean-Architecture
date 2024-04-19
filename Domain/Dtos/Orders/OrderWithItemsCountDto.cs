using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Dtos.Orders;

public sealed record OrderWithPriceDto(Order Order, Money Money);