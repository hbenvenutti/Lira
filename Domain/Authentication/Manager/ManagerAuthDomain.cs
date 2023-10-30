using Lira.Common.Types;
using Lira.Domain.Domains.Manager;

namespace Lira.Domain.Authentication.Manager;

public struct ManagerAuthDomain
{
    # region ---- properties ---------------------------------------------------

    public Guid Id { get; init; }
    public Username Username { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public ManagerAuthDomain(
        Guid id,
        string username
    )
    {
        Id = id;
        Username = username;
    }

    # endregion

    # region ---- operator -----------------------------------------------------

    public static implicit operator ManagerAuthDomain(ManagerDomain domain) =>
        new(
            id: domain.Id,
            username: domain.Username
        );

    # endregion
}
