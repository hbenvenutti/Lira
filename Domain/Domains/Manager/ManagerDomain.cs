using Lira.Common.Types;
using Lira.Domain.Domains.Base;
using Lira.Domain.Domains.Person;
using Lira.Domain.Enums;

namespace Lira.Domain.Domains.Manager;

public class ManagerDomain : BaseDomain
{
    # region ---- properties ---------------------------------------------------

    public Username Username { get; init; }
    public string Password { get; init; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public Guid PersonId { get; init; }
    public PersonDomain? Person { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public ManagerDomain(
        Guid id,
        DateTime createdAt,
        Username username,
        string password,
        Guid personId,
        PersonDomain? person = null,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null,
        DomainStatus status = DomainStatus.Active
    )
        : base(id, createdAt, status, updatedAt, deletedAt)
    {
        Username = username;
        Password = password;
        PersonId = personId;
        Person = person;
    }

    # endregion

    # region ---- factory ------------------------------------------------------

    public static ManagerDomain Create(
        Username username,
        Password password,
        Guid personId
    )
    {
        return new ManagerDomain(
            id: Guid.Empty,
            createdAt: DateTime.UtcNow,
            username: username,
            password: password.Hash,
            personId: personId
        );
    }

    # endregion
}
