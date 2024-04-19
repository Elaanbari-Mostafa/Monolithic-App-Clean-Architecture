using Domain.Dtos.Products;

namespace Presentation.Contracts.Orders;

public sealed record CreateOrderRequest(
                             HashSet<ProductIdQtyDto> Products);