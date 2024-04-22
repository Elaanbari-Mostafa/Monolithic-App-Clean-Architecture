using System.Data;

namespace Domain.Repositories;

public interface IUnitOfWork
{
    /// <summary>
    /// save change async to data base
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Task</returns>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
    /// <summary>
    /// Create a transaction
    /// </summary>
    /// <returns>IDbTransaction</returns>
    IDbTransaction BeginTransaction();
}