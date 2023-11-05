using Lira.Domain.Domains.Base;
using Lira.Domain.Domains.Person;
using Lira.Domain.Enums;

namespace Lira.Domain.Domains.Medium;

public class MediumDomain : BaseDomain
{
    # region ---- properties ---------------------------------------------------

    public DateTime? FirstAmaci { get; init; }
    public DateTime? LastAmaci { get; init; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public Guid PersonId { get; init; }
    public PersonDomain? Person { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public MediumDomain(
        Guid id,
        Guid personId,
        DateTime createdAt,
        DomainStatus status = DomainStatus.Active,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null,
        DateTime? firstAmaci = null,
        DateTime? lastAmaci = null,
        PersonDomain? person = null
    )
    : base(
        id,
        createdAt,
        status,
        updatedAt,
        deletedAt
    )
    {
        FirstAmaci = firstAmaci;
        LastAmaci = lastAmaci;
        PersonId = personId;
        Person = person;
    }

    # endregion

    # region ---- factory ------------------------------------------------------

    public static MediumDomain Create(
        Guid personId,
        DateTime? firstAmaci = null,
        DateTime? lastAmaci = null
    )
    {
        return new MediumDomain(
            id: Guid.Empty,
            personId: personId,
            createdAt: DateTime.UtcNow,
            firstAmaci: firstAmaci,
            lastAmaci: lastAmaci,
            person: null
        );
    }

    # endregion
}
