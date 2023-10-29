using Lira.Common.Types;
using Lira.Data.Contexts;
using Lira.Data.Entities;
using Lira.Data.Enums;
using Lira.Data.Extensions;
using Lira.Domain.Domains.Manager;
using Microsoft.EntityFrameworkCore;

namespace Lira.Data.Repositories;

public class ManagerRepository : IManagerRepository
{
    private readonly IDbContext _dbContext;

    public ManagerRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    # region ---- write --------------------------------------------------------

    public async Task<ManagerDomain> CreateAsync(ManagerDomain managerDomain)
    {
        var manager = (ManagerEntity) managerDomain;

        await _dbContext.Managers.AddAsync(manager);

        await _dbContext.SaveChangesAsync();

        return (ManagerDomain) manager;
    }

    public async Task<ManagerDomain> UpdateAsync(ManagerDomain managerDomain)
    {
        var manager = (ManagerEntity) managerDomain;

        _dbContext.Managers.Update(manager);

        await _dbContext.SaveChangesAsync();

        return (ManagerDomain) manager;
    }

    public async Task DeleteAsync(ManagerDomain managerDomain)
    {
        var manager = (ManagerEntity) managerDomain;

        manager.DeleteEntity();

        _dbContext.Managers.Update(manager);

        await _dbContext.SaveChangesAsync();
    }

    # endregion

    # region ---- read ---------------------------------------------------------

    public async Task<ManagerDomain?> FindByUsernameAsync(
        Username username,
        bool includePerson = false,
        bool includeDeleted = false
    )
    {
        var query = _dbContext.Managers
            .AsNoTracking()
            .Where(manager => manager.Username == username);

        if (includePerson)
        {
            query = query
                .AsNoTracking()
                .Include(manager => manager.Person);
        }

        if (!includeDeleted)
        {
            query = query
                .AsNoTracking()
                .Where(manager => manager.Status != EntityStatus.Deleted);
        }

        var manager = await query.SingleOrDefaultAsync();

        if (manager is null) { return null; }

        return (ManagerDomain) manager;
    }

    public async Task<IEnumerable<ManagerDomain>> FindAllAsync(
        bool includePerson = false,
        bool includeDeleted = false
    )
    {
        var query = _dbContext.Managers
            .AsNoTracking();

        if (includePerson)
        {
            query = query
                .AsNoTracking()
                .Include(manager => manager.Person);
        }

        if (!includeDeleted)
        {
            query = query
                .AsNoTracking()
                .Where(manager => manager.Status != EntityStatus.Deleted);
        }

        var managers = await query.ToListAsync();

        return managers.Select(manager => (ManagerDomain) manager);
    }

    # endregion
}
