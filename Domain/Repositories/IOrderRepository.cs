using Domain.Entities;

namespace Domain.Repositories;

public interface IOrderRepository
{
    void AddOrder(Order order);
}