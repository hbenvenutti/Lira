using Lira.Data.Config;
using Lira.Data.Entities;
using Lira.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Lira.Data.Contexts;

public sealed class LiraDbContext : DbContext, IDbContext
{
    # region ---- tables -------------------------------------------------------

    public DbSet<PersonEntity> Persons { get; set; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public LiraDbContext(DbContextOptions<LiraDbContext> options)
        : base(options)
    {
        Persons = Set<PersonEntity>();
    }

    # endregion

    # region ---- build tables -------------------------------------------------

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonEntityConfig());
    }

    # endregion

    # region ---- save changes -------------------------------------------------

    public override Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = new()
    )
    {
        OnSaveChanges();

        return base
            .SaveChangesAsync(cancellationToken);
    }

    private void OnSaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity)
                entry.HandleOnSaveChanges();
        }
    }

    # endregion
}
