using Lira.Data.Entities;
using Lira.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lira.Data.Extensions;

public static class EntityExtension
{
    # region ---- command ------------------------------------------------------

    private static readonly IDictionary<EntityState, Action<BaseEntity>>
        Commands = new Dictionary<EntityState, Action<BaseEntity>>
        {
            [EntityState.Added] = entity =>
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.Id = Guid.NewGuid();
            },

            [EntityState.Modified] = entity => entity.UpdatedAt = DateTime.UtcNow
        };

    # endregion

    public static void HandleOnSaveChanges(this EntityEntry entry)
    {
        if (entry.Entity is not BaseEntity entity) return;

        if (!Commands.ContainsKey(entry.State)) return;

        Commands[entry.State](entity);
    }

    public static void DeleteEntity<T>(this T entity) where T : BaseEntity
    {
        entity.DeletedAt = DateTime.UtcNow;
        entity.Status = EntityStatus.Deleted;
    }
}
