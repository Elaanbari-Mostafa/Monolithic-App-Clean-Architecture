using Domain.Entities;

namespace Domain.Repositories;

public interface IProductRepository : IRepository
{
    void AddProduct(Product product);
}