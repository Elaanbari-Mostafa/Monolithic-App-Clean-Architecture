namespace Infrastructure.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext) => _dbContext = ThrowIfNull(dbContext);

    public async Task<User?> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => await _dbContext.Set<User>().FindAsync(new object?[] { id }, cancellationToken);

    public void AddUser(User user) => _dbContext.Set<User>().Add(user);

    public void UpdateUser(User user) => _dbContext.Set<User>().Update(user);

    public void DeleteUser(User user) => _dbContext.Set<User>().Remove(user);

    public async Task<bool> IsEmailUniqueAsync(Email email, CancellationToken cancellationToken = default)
        => await _dbContext.Set<User>()
                     .AsNoTracking()
                     .AnyAsync(u => u.Email.Equals(email), cancellationToken);

    public async Task<User?> GetUserByEmailAsync(Email email, CancellationToken cancellationToken = default)
        => await _dbContext.Set<User>()
                     .AsNoTracking()
                     .FirstOrDefaultAsync(u => u.Email.Equals(email), cancellationToken);
}