using Lira.Domain.Domains.Base;
using Lira.Domain.Domains.Person;
using Lira.Domain.Enums;

namespace Lira.Domain.Domains.Manager;

public class ManagerDomain : BaseDomain
{
    # region ---- properties ---------------------------------------------------

    public string Username { get; init; }
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
        string username,
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
}
