using Domain.Entities;

namespace Domain.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    void AddOrder(Order order);
    void UpdateOrder(Order order);
}