using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using static Domain.Exceptions.CustomArgumentNullException;

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
}
