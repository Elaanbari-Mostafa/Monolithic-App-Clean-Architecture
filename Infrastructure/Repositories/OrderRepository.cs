using Domain.Dtos.Orders;

namespace Infrastructure.Repositories;

public sealed class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
        => _context = ThrowIfNull(context);

    public void AddOrder(Order order)
    {
        _context.Set<Order>().Add(order);
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        var order = await _context.Set<Order>().FirstOrDefaultAsync(o => o.Id == id);
        return order;
    }

    public void UpdateOrder(Order order)
    {
        _context.Set<Order>().Update(order);
    }

    public async Task<OrderWithPriceDto?> GetOrderWithTotalPrice(Guid id)
    {
        var order = await _context.Set<Order>()
            .Where(o => o.Id == id)
            .Select(o => new OrderWithPriceDto(
                o,
                Money.Create(
                    o.LineItems.Sum(x => x.Money.Price),
                    o.LineItems.First().Money.Currency).Value))
            .FirstOrDefaultAsync();

        return order;
    }
}