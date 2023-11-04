using BrazilianTypes.Types;
using Lira.Domain.Domains.Base;
using Lira.Domain.Domains.Person;
using Lira.Domain.Enums;

namespace Lira.Domain.Domains.Address;

public class AddressDomain : BaseDomain
{
    # region ---- properties ---------------------------------------------------

    public Text Street { get; set; }
    public Text Number { get; set; }
    public Text Neighborhood { get; set; }
    public Text City { get; set; }
    public Uf State { get; set; }
    public ZipCode ZipCode { get; set; }
    public string? Complement { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public Guid PersonId { get; set; }
    public PersonDomain? Person { get; set; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public AddressDomain(
        Guid id,
        Text street,
        Text number,
        Text neighborhood,
        Text city,
        Uf state,
        ZipCode zipCode,
        Guid personId,
        DateTime createdAt,
        DomainStatus status = DomainStatus.Active,
        PersonDomain? person = null,
        string? complement = null,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null
    )
    : base(id,
        createdAt,
        status,
        updatedAt,
        deletedAt
    )
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        ZipCode = zipCode;
        PersonId = personId;
        Person = person;
        Complement = complement;
    }

    # endregion

    # region ---- factories ----------------------------------------------------

    public static AddressDomain Create(
        Text street,
        Text number,
        Text neighborhood,
        Text city,
        Uf state,
        ZipCode zipCode,
        Guid personId,
        string? complement = null
    )
    {
        return new AddressDomain(
            id: Guid.Empty,
            street: street,
            number: number,
            neighborhood: neighborhood,
            city: city,
            state: state,
            zipCode: zipCode,
            personId: personId,
            createdAt: DateTime.UtcNow,
            complement: complement
        );
    }

    # endregion
}
