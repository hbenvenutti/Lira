using Lira.Data.Contexts;
using Lira.Data.Entities;
using Lira.Data.Extensions;
using Lira.Domain.Domains.Medium;

namespace Lira.Data.Repositories;

public class MediumRepository : IMediumRepository
{
    private readonly IDbContext _dbContext;

    public MediumRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MediumDomain> CreateAsync(MediumDomain domain)
    {
        var medium = (MediumEntity) domain;

        await _dbContext.Mediums.AddAsync(medium);

        await _dbContext.SaveChangesAsync();

        return (MediumDomain) medium;
    }

    public async Task<MediumDomain> UpdateAsync(MediumDomain domain)
    {
        var medium = (MediumEntity) domain;

        _dbContext.Mediums.Update(medium);

        await _dbContext.SaveChangesAsync();

        return (MediumDomain) medium;
    }

    public async Task DeleteAsync(MediumDomain domain)
    {
        var medium = (MediumEntity) domain;

        medium.DeleteEntity();

        _dbContext.Mediums.Update(medium);

        await _dbContext.SaveChangesAsync();
    }
}
