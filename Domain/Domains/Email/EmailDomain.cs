using Lira.Domain.Domains.Base;
using Lira.Domain.Domains.Person;
using Lira.Domain.Enums;
using EmailValue = BrazilianTypes.Types.Email;

namespace Lira.Domain.Domains.Email;

public class EmailDomain : BaseDomain
{
    # region ---- properties --------------------------------------------------

    public EmailValue Address { get; set; }
    public EmailType Type { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public Guid PersonId { get; set; }
    public PersonDomain? Person { get; set; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public EmailDomain(
        Guid id,
        EmailValue address,
        EmailType type,
        Guid personId,
        DateTime createdAt,
        PersonDomain? person = null,
        DomainStatus status = DomainStatus.Active,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null
    ) : base(
        id,
        createdAt,
        status,
        updatedAt,
        deletedAt
    )
    {
        Address = address;
        Type = type;
        PersonId = personId;
        Person = person;
    }

    # endregion

    # region ---- factory ------------------------------------------------------

    public static EmailDomain Create(
        EmailValue address,
        EmailType type,
        Guid personId
    ) => new(
        id: Guid.Empty,
        address: address,
        type: type,
        personId: personId,
        createdAt: DateTime.UtcNow
    );

    # endregion
}
