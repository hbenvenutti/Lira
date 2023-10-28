using System.Diagnostics.CodeAnalysis;
using Lira.Data.Contexts;
using Lira.Data.Entities;
using Lira.Data.Enums;
using Lira.Domain.Religion.Structs;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lira.Data.Migrations;

/// <inheritdoc />
[ExcludeFromCodeCoverage]
public partial class SeedOrixas : Migration
{
    private readonly IDbContext _dbContext;

    public SeedOrixas(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    protected async override void Up(MigrationBuilder migrationBuilder)
    {
        var xango = new OrixaEntity
        {
            Id = Guid.Empty,
            Name = Orixas.Xango,
            Status = EntityStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.Orixas.AddAsync(xango);

        var oxum = new OrixaEntity
        {
            Id = Guid.Empty,
            Name = Orixas.Oxum,
            Status = EntityStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.Orixas.AddAsync(oxum);

        var oxossi = new OrixaEntity
        {
            Id = Guid.Empty,
            Name = Orixas.Oxossi,
            Status = EntityStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.Orixas.AddAsync(oxossi);

        var iansa = new OrixaEntity
        {
            Id = Guid.Empty,
            Name = Orixas.Iansa,
            Status = EntityStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.Orixas.AddAsync(iansa);

        var ogum = new OrixaEntity
        {
            Id = Guid.Empty,
            Name = Orixas.Ogum,
            Status = EntityStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.Orixas.AddAsync(ogum);

        var egunita = new OrixaEntity
        {
            Id = Guid.Empty,
            Name = Orixas.Egunita,
            Status = EntityStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.Orixas.AddAsync(egunita);

        var obaluaie = new OrixaEntity
        {
            Id = Guid.Empty,
            Name = Orixas.Obaluaie,
            Status = EntityStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.Orixas.AddAsync(obaluaie);

        var nana = new OrixaEntity
        {
            Id = Guid.Empty,
            Name = Orixas.Nana,
            Status = EntityStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.Orixas.AddAsync(nana);

        var iemanja = new OrixaEntity
        {
            Id = Guid.Empty,
            Name = Orixas.Iemanja,
            Status = EntityStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.Orixas.AddAsync(iemanja);

        var omolu = new OrixaEntity
        {
            Id = Guid.Empty,
            Name = Orixas.Omolu,
            Status = EntityStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.Orixas.AddAsync(omolu);

        var oxala = new OrixaEntity
        {
            Id = Guid.Empty,
            Name = Orixas.Oxala,
            Status = EntityStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.Orixas.AddAsync(oxala);

        var oya = new OrixaEntity
        {
            Id = Guid.Empty,
            Name = Orixas.Oya,
            Status = EntityStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.Orixas.AddAsync(oya);

        var oxumare = new OrixaEntity
        {
            Id = Guid.Empty,
            Name = Orixas.Oxumare,
            Status = EntityStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.Orixas.AddAsync(oxumare);

        var oba = new OrixaEntity
        {
            Id = Guid.Empty,
            Name = Orixas.Oba,
            Status = EntityStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

        await _dbContext.Orixas.AddAsync(oba);

        await _dbContext.SaveChangesAsync();
    }

    /// <inheritdoc />
    protected async override void Down(MigrationBuilder migrationBuilder)
    {
        _dbContext.Orixas.RemoveRange(_dbContext.Orixas);
        await _dbContext.SaveChangesAsync();
    }
}
