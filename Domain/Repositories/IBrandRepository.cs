using Domain.Entities;
using System.Threading;

namespace Domain.Repositories;

public interface IBrandRepository
{
    Task<Brand?> GetBrandByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
