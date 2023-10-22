using Lira.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lira.Data.Contexts;

public interface IDbContext
{
    DbSet<PersonEntity> Persons { get; set; }
    DbSet<EmailEntity> Emails { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
