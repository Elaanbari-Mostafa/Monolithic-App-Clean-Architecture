namespace Presentation.Contracts.Orders;

public sealed record CreateOrderRequest(
                            HashSet<Guid> ProductIds,
                            Guid CustomerId);