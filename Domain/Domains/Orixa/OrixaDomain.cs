using Lira.Domain.Domains.Base;
using Lira.Domain.Domains.PersonOrixa;
using Lira.Domain.Enums;

namespace Lira.Domain.Domains.Orixa;

public class OrixaDomain : BaseDomain
{
    # region ---- properties ---------------------------------------------------

    public string Name { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public IEnumerable<PersonOrixaDomain>? PersonOrixas { get; set; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public OrixaDomain(
        Guid id,
        string name,
        DateTime createdAt,
        IEnumerable<PersonOrixaDomain>? personOrixas = null,
        DomainStatus status = DomainStatus.Active,
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
        Name = name;
        PersonOrixas = personOrixas;
    }

    # endregion
}
