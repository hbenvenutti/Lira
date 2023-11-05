using Lira.Data.Contexts;
using Lira.Domain.Domains.Orixa;
using Microsoft.EntityFrameworkCore;

namespace Lira.Data.Repositories;

public class OrixaRepository : IOrixaRepository
{
    private readonly IDbContext _dbContext;

    public OrixaRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrixaDomain?> FindByIdAsync(Guid id)
    {
        var orixa = await _dbContext.Orixas
            .AsNoTracking()
            .SingleOrDefaultAsync(orixa => orixa.Id == id);

        return orixa is null
            ? null
            : (OrixaDomain) orixa;
    }
}
