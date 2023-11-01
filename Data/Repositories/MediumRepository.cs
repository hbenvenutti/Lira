using BrazilianTypes.Types;
using Lira.Data.Contexts;
using Lira.Data.Entities;
using Lira.Data.Extensions;
using Lira.Domain.Domains.Medium;
using Microsoft.EntityFrameworkCore;

namespace Lira.Data.Repositories;

public class MediumRepository : IMediumRepository
{
    private readonly IDbContext _dbContext;

    # region ---- constructor --------------------------------------------------

    public MediumRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    # endregion

    # region ---- create -------------------------------------------------------

    public async Task<MediumDomain> CreateAsync(MediumDomain domain)
    {
        var medium = (MediumEntity) domain;

        await _dbContext.Mediums.AddAsync(medium);

        await _dbContext.SaveChangesAsync();

        return (MediumDomain) medium;
    }

    # endregion

    # region ---- update -------------------------------------------------------

    public async Task<MediumDomain> UpdateAsync(MediumDomain domain)
    {
        var medium = (MediumEntity) domain;

        _dbContext.Mediums.Update(medium);

        await _dbContext.SaveChangesAsync();

        return (MediumDomain) medium;
    }

    # endregion

    # region ---- delete -------------------------------------------------------

    public async Task DeleteAsync(MediumDomain domain)
    {
        var medium = (MediumEntity) domain;

        medium.DeleteEntity();

        _dbContext.Mediums.Update(medium);

        await _dbContext.SaveChangesAsync();
    }

    # endregion

    # region ---- find by id ---------------------------------------------------

    public async Task<MediumDomain?> FindByIdAsync(Guid id)
    {
        var medium = await _dbContext.Mediums
            .AsNoTracking()
            .Include(medium => medium.Person)
            .SingleOrDefaultAsync(medium => medium.Id == id);

        return medium is null
            ? null
            : (MediumDomain) medium;
    }

    # endregion

    # region ---- find by cpf --------------------------------------------------

    public async Task<MediumDomain?> FindByCpfAsync(Cpf cpf)
    {
        var medium = await _dbContext.Mediums
            .AsNoTracking()
            .Include(medium => medium.Person)
            .SingleOrDefaultAsync(medium => medium.Person!.Cpf.Equals(cpf));

        return medium is null
            ? null
            : (MediumDomain) medium;
    }

    # endregion
}
