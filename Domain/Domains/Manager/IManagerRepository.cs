using Lira.Common.Types;

namespace Lira.Domain.Domains.Manager;

public interface IManagerRepository
{
    # region ---- write --------------------------------------------------------

    public Task<ManagerDomain> CreateAsync(ManagerDomain managerDomain);
    public Task<ManagerDomain> UpdateAsync(ManagerDomain managerDomain);
    public Task DeleteAsync(ManagerDomain managerDomain);

    # endregion

    # region ---- read ---------------------------------------------------------

    public Task<ManagerDomain?> FindByUsernameAsync(
        Username username,
        bool includePerson = false,
        bool includeDeleted = false
    );

    public Task<IEnumerable<ManagerDomain>> FindAllAsync(
        bool includePerson = false,
        bool includeDeleted = false
    );

    # endregion
}
