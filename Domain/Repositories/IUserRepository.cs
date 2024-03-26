using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Get user by id
    /// </summary>
    /// <param name="Id"></param>
    /// <returns>User instance</returns>
    Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Add user
    /// </summary>
    /// <param name="user"></param>
    void AddUser(User user);
    /// <summary>
    /// Update user
    /// </summary>
    /// <param name="user"></param>
    void UpdateUser(User user);
    /// <summary>
    /// is email is unique
    /// </summary>
    /// <param name="email"></param>
    /// <returns>bool</returns>
    Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken);
    /// <summary>
    /// get user by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns>user</returns>
    Task<User?> GetUserByEmailAsync(Email email, CancellationToken cancellationToken);
}