using Domain.Dtos.Orders;
using Domain.Entities;

namespace Domain.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    void AddOrder(Order order);
    Task<OrderWithPriceDto?> GetOrderWithTotalPrice(Guid id);
    void UpdateOrder(Order order);
}