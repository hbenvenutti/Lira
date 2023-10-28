using Lira.Data.Config;
using Lira.Data.Entities;
using Lira.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Lira.Data.Contexts;

public sealed class LiraDbContext : DbContext, IDbContext
{
    # region ---- tables -------------------------------------------------------

    public DbSet<PersonEntity> Persons { get; set; }
    public DbSet<EmailEntity> Emails { get; set; }
    public DbSet<PhoneEntity> Phones { get; set; }
    public DbSet<OrixaEntity> Orixas { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<ManagerEntity> Managers { get; set; }
    public DbSet<MediumEntity> Mediums { get; set; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public LiraDbContext(DbContextOptions<LiraDbContext> options)
        : base(options)
    {
        Persons = Set<PersonEntity>();
        Emails = Set<EmailEntity>();
        Phones = Set<PhoneEntity>();
        Orixas = Set<OrixaEntity>();
        Addresses = Set<AddressEntity>();
        Managers = Set<ManagerEntity>();
        Mediums = Set<MediumEntity>();
    }

    # endregion

    # region ---- build tables -------------------------------------------------

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonEntityConfig());
        modelBuilder.ApplyConfiguration(new EmailEntityConfig());
        modelBuilder.ApplyConfiguration(new PhoneEntityConfig());
        modelBuilder.ApplyConfiguration(new OrixaEntityConfig());
        modelBuilder.ApplyConfiguration(new AddressEntityConfig());
        modelBuilder.ApplyConfiguration(new ManagerEntityConfig());
        modelBuilder.ApplyConfiguration(new MediumEntityConfig());

        base.OnModelCreating(modelBuilder);
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
