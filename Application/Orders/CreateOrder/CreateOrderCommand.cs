using Domain.Dtos.Products;

namespace Application.Orders.CreateOrder;

public sealed record CreateOrderCommand(
                            HashSet<ProductIdQtyDto> Products) : ICommand<Guid>;