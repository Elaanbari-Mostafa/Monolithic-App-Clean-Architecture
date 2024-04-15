using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        var order = await _context.Set<Order>().FirstOrDefaultAsync(o => o.Id == id);
        return order;
    }

    public void UpdateOrder(Order order)
    {
        _context.Set<Order>().Update(order);
    }
}
