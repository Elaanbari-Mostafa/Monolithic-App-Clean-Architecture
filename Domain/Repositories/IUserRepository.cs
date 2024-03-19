using Domain.Entities;

namespace Domain.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Get user by id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns>User instance</returns>
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
}
