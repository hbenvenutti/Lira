using Lira.Domain.Domains.Base;
using Lira.Domain.Domains.Orixa;
using Lira.Domain.Domains.Person;
using Lira.Domain.Enums;
using Lira.Domain.Religion.Enums;

namespace Lira.Domain.Domains.PersonOrixa;

public class PersonOrixaDomain : BaseDomain
{
    # region ---- properties ---------------------------------------------------

    public OrixaType Type { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public Guid PersonId { get; set; }
    public PersonDomain? Person { get; set; }

    public Guid OrixaId { get; set; }
    public OrixaDomain? Orixa { get; set; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public PersonOrixaDomain(
        Guid id,
        OrixaType type,
        Guid personId,
        Guid orixaId,
        DateTime createdAt,
        OrixaDomain? orixa = null,
        DomainStatus status = DomainStatus.Active,
        PersonDomain? person = null,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null
    )
    : base(
        id,
        createdAt,
        status,
        updatedAt,
        deletedAt
    )
    {
        Type = type;
        PersonId = personId;
        Person = person;
        OrixaId = orixaId;
        Orixa = orixa;
    }

    # endregion

    # region ---- factory ------------------------------------------------------

    public static PersonOrixaDomain Create(
        OrixaType type,
        Guid personId,
        Guid orixaId
    )
    {
        return new PersonOrixaDomain(
            id: Guid.Empty,
            type: type,
            personId: personId,
            orixaId: orixaId,
            createdAt: DateTime.UtcNow
        );
    }

    # endregion
}
