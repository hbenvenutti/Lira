using Lira.Data.Contexts;
using Lira.Data.Entities;
using Lira.Data.Enums;
using Lira.Domain.Religion.Structs;

namespace Lira.Data.Extensions;

public static class DatabaseExtension
{
    public static async Task SeedDatabaseAsync(this IDbContext dbContext)
    {
        await dbContext.SeedOrixasAsync();
    }

    # region ---- orixas -------------------------------------------------------

    private static async Task SeedOrixasAsync(this IDbContext dbContext)
    {
        if (dbContext.Orixas.Any()) { return; }

        var orixas = new List<OrixaEntity>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Name = Orixas.Xango,
                Status = EntityStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = Orixas.Oxum,
                Status = EntityStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = Orixas.Oxossi,
                Status = EntityStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = Orixas.Iansa,
                Status = EntityStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = Orixas.Ogum,
                Status = EntityStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = Orixas.Egunita,
                Status = EntityStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = Orixas.Oxala,
                Status = EntityStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = Orixas.Oba,
                Status = EntityStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = Orixas.Oya,
                Status = EntityStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = Orixas.Oxumare,
                Status = EntityStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = Orixas.Obaluaie,
                Status = EntityStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = Orixas.Nana,
                Status = EntityStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = Orixas.Iemanja,
                Status = EntityStatus.Active,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                Id = Guid.NewGuid(),
                Name = Orixas.Omolu,
                Status = EntityStatus.Active,
                CreatedAt = DateTime.UtcNow
            }
        };

        await dbContext.Orixas.AddRangeAsync(orixas);
        await dbContext.SaveChangesAsync();
    }

    # endregion
}
