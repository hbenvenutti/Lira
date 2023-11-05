using BrazilianTypes.Types;
using Lira.Domain.Domains.Base;
using Lira.Domain.Domains.Person;
using Lira.Domain.Enums;

namespace Lira.Domain.Domains.Phones;

public class PhoneDomain : BaseDomain
{
    # region ---- properties ---------------------------------------------------

    public string Number { get; set; }
    public string Ddd { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public Guid PersonId { get; set; }
    public PersonDomain? Person { get; set; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public PhoneDomain(
        Phone phone,
        Guid id,
        DateTime createdAt,
        Guid personId,
        PersonDomain? person = null,
        DomainStatus status = DomainStatus.Active,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null
    )
    : base(id,
        createdAt,
        status,
        updatedAt,
        deletedAt)
    {
        PersonId = personId;
        Person = person;
        Number = phone.Number;
        Ddd = phone.Ddd;
    }

    # endregion

    # region ---- factory ------------------------------------------------------

    public static PhoneDomain Create(
        Phone phone,
        Guid personId
    )
    {
        return new PhoneDomain(
            phone: phone,
            id: Guid.Empty,
            createdAt: DateTime.UtcNow,
            personId: personId
        );
    }

    # endregion
}
