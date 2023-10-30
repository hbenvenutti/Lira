using Lira.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lira.Data.Contexts;

public interface IDbContext
{
    # region ---- tables -------------------------------------------------------

    DbSet<PersonEntity> People { get; set; }
    DbSet<EmailEntity> Emails { get; set; }
    DbSet<PhoneEntity> Phones { get; set; }
    DbSet<OrixaEntity> Orixas { get; set; }
    DbSet<AddressEntity> Addresses { get; set; }
    DbSet<ManagerEntity> Managers { get; set; }
    DbSet<MediumEntity> Mediums { get; set; }
    DbSet<PersonOrixaEntity> PersonOrixas { get; set; }

    # endregion

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
