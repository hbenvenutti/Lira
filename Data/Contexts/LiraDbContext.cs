using System.Diagnostics.CodeAnalysis;
using Lira.Data.Config;
using Lira.Data.Entities;
using Lira.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Lira.Data.Contexts;

[ExcludeFromCodeCoverage]
public sealed class LiraDbContext : DbContext, IDbContext
{
    # region ---- tables -------------------------------------------------------

    public DbSet<PersonEntity> People { get; set; }
    public DbSet<EmailEntity> Emails { get; set; }
    public DbSet<PhoneEntity> Phones { get; set; }
    public DbSet<OrixaEntity> Orixas { get; set; }
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<ManagerEntity> Managers { get; set; }
    public DbSet<MediumEntity> Mediums { get; set; }
    public DbSet<PersonOrixaEntity> PersonOrixas { get; set; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public LiraDbContext(DbContextOptions<LiraDbContext> options)
        : base(options)
    {
        People = Set<PersonEntity>();
        Emails = Set<EmailEntity>();
        Phones = Set<PhoneEntity>();
        Orixas = Set<OrixaEntity>();
        Addresses = Set<AddressEntity>();
        Managers = Set<ManagerEntity>();
        Mediums = Set<MediumEntity>();
        PersonOrixas = Set<PersonOrixaEntity>();
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
        modelBuilder.ApplyConfiguration(new PersonOrixaEntityConfig());

        base.OnModelCreating(modelBuilder);
    }

    # endregion

    # region ---- save changes -------------------------------------------------

    public override Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = new()
    )
    {
        OnSaveChanges();

        return base.SaveChangesAsync(cancellationToken);
    }

    private void OnSaveChanges()
    {
        ChangeTracker.Entries().ToList().ForEach(entry =>
        {
            if (entry.Entity is not BaseEntity) return;

            entry.HandleOnSaveChanges();
        });
    }

    # endregion
}
