using Lira.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lira.Data.Contexts;

public interface IDbContext
{
    # region ---- tables -------------------------------------------------------

    DbSet<PersonEntity> Persons { get; set; }
    DbSet<EmailEntity> Emails { get; set; }
    DbSet<PhoneEntity> Phones { get; set; }
    DbSet<OrixaEntity> Orixas { get; set; }

    # endregion

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
