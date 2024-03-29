using Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public sealed class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        DbContext? dbContext = eventData.Context;

        if (dbContext is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        IEnumerable<EntityEntry<IAuditableEntity>> entities = dbContext
            .ChangeTracker
            .Entries<IAuditableEntity>();

        foreach (EntityEntry<IAuditableEntity> entityEntry in entities)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(e => e.CreatedOnUtc).CurrentValue = DateTime.UtcNow;
            }
            else if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(e => e.ModifiedOnUtc).CurrentValue = DateTime.UtcNow;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}