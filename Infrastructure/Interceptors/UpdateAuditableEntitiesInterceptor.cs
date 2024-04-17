using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor, IDbInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is { })
        {
            UpdateAuditableEntities(dbContext);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    static void UpdateAuditableEntities(DbContext dbContext)
    {
        DateTime utcNow = DateTime.UtcNow;
        IEnumerable<EntityEntry<IAuditableEntity>> entities = dbContext
            .ChangeTracker
            .Entries<IAuditableEntity>();

        foreach (EntityEntry<IAuditableEntity> entry in entities)
        {
            if (entry.State == EntityState.Added)
            {
                SetCurrentPropertyValue(
                    entry, nameof(IAuditableEntity.CreatedOnUtc), utcNow);
            }
            else if (entry.State == EntityState.Modified)
            {
                SetCurrentPropertyValue(
                    entry, nameof(IAuditableEntity.ModifiedOnUtc), utcNow);
            }
        }
    }

    static void SetCurrentPropertyValue(
        EntityEntry entityEntry,
        string propertyName,
        DateTime utcNow)
         => entityEntry
            .Property(propertyName).CurrentValue = utcNow;
}