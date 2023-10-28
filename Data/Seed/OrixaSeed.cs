using Lira.Data.Entities;
using Lira.Data.Enums;
using Lira.Domain.Religion.Structs;
using Microsoft.EntityFrameworkCore;

namespace Lira.Data.Seed;

public static class OrixaSeed
{
    public static void SeedOrixas(this ModelBuilder builder)
    {
        builder
            .Entity<OrixaEntity>()
            .HasData(
                new OrixaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Orixas.Xango,
                    Status = EntityStatus.Active,
                    CreatedAt = DateTime.UtcNow
                },

                new OrixaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Orixas.Oxum,
                    Status = EntityStatus.Active,
                    CreatedAt = DateTime.UtcNow
                },

                new OrixaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Orixas.Oxossi,
                    Status = EntityStatus.Active,
                    CreatedAt = DateTime.UtcNow
                },

                new OrixaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Orixas.Iansa,
                    Status = EntityStatus.Active,
                    CreatedAt = DateTime.UtcNow
                },

                new OrixaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Orixas.Ogum,
                    Status = EntityStatus.Active,
                    CreatedAt = DateTime.UtcNow
                },

                new OrixaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Orixas.Egunita,
                    Status = EntityStatus.Active,
                    CreatedAt = DateTime.UtcNow
                },
                new OrixaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Orixas.Obaluaie,
                    Status = EntityStatus.Active,
                    CreatedAt = DateTime.UtcNow
                },

                new OrixaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Orixas.Nana,
                    Status = EntityStatus.Active,
                    CreatedAt = DateTime.UtcNow
                },

                new OrixaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Orixas.Iemanja,
                    Status = EntityStatus.Active,
                    CreatedAt = DateTime.UtcNow
                },

                new OrixaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Orixas.Oxala,
                    Status = EntityStatus.Active,
                    CreatedAt = DateTime.UtcNow
                },

                new OrixaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Orixas.Oya,
                    Status = EntityStatus.Active,
                    CreatedAt = DateTime.UtcNow
                },

                new OrixaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Orixas.Oxumare,
                    Status = EntityStatus.Active,
                    CreatedAt = DateTime.UtcNow
                },

                new OrixaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Orixas.Oba,
                    Status = EntityStatus.Active,
                    CreatedAt = DateTime.UtcNow
                },

                new OrixaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = Orixas.Omolu,
                    Status = EntityStatus.Active,
                    CreatedAt = DateTime.UtcNow
                }
            );
    }
}
