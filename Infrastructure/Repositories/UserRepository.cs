using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using static Domain.Exceptions.CustomArgumentNullException;

namespace Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
        => _dbContext = ThrowIfNull(dbContext);

    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbContext.Set<User>()
                    .Where(u => u.Id == id)
                    .FirstOrDefaultAsync(cancellationToken);

    public void AddUser(User user) => _dbContext.Set<User>().Add(user);

    public void UpdateUser(User user) => _dbContext.Set<User>().Update(user);

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
        => await _dbContext.Set<User>()
                     .AsNoTracking()
                     .AnyAsync(u => u.Email.Equals(email), cancellationToken);
}