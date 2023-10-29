namespace Lira.Domain.Domains.Manager;

public interface IManagerRepository
{
    # region ---- write --------------------------------------------------------

    public Task<ManagerDomain> CreateAsync(ManagerDomain managerDomain);
    public Task<ManagerDomain> UpdateAsync(ManagerDomain managerDomain);
    public Task DeleteAsync(ManagerDomain managerDomain);

    # endregion

    # region ---- read ---------------------------------------------------------

    public Task<ManagerDomain?> FindByUsernameAsync(string username);
    public Task<IEnumerable<ManagerDomain>> FindAllAsync();

    # endregion
}
