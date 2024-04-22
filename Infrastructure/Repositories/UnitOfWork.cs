using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace Infrastructure.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext) => _dbContext = ThrowIfNull(dbContext);

    public IDbTransaction BeginTransaction()
    {
        var transaction = _dbContext.Database.BeginTransaction();
        return transaction.GetDbTransaction();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default) => _dbContext.SaveChangesAsync(cancellationToken);
}