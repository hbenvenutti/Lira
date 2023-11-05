using BrazilianTypes.Types;
using Lira.Domain.Domains.Address;
using Lira.Domain.Domains.Base;
using Lira.Domain.Domains.Emails;
using Lira.Domain.Domains.Manager;
using Lira.Domain.Domains.Medium;
using Lira.Domain.Domains.PersonOrixa;
using Lira.Domain.Domains.Phones;
using Lira.Domain.Enums;

namespace Lira.Domain.Domains.Person;

public class PersonDomain : BaseDomain
{
    # region ---- properties ---------------------------------------------------

    public Cpf Cpf { get; set; }
    public Name Name { get; set; }
    public Name Surname { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public ManagerDomain? Manager { get; set; }
    public MediumDomain? Medium { get; set; }

    public IEnumerable<AddressDomain>? Addresses { get; set; }
    public IEnumerable<PhoneDomain>? Phones { get; set; }
    public IEnumerable<PersonOrixaDomain>? PersonOrixas { get; set; }
    public IEnumerable<EmailDomain>? Emails { get; set; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public PersonDomain(
        Guid id,
        DateTime createdAt,
        Cpf cpf,
        Name name,
        Name surname,
        IEnumerable<EmailDomain>? emails = null,
        IEnumerable<PhoneDomain>? phones = null,
        IEnumerable<PersonOrixaDomain>? personOrixas = null,
        ManagerDomain? manager = null,
        MediumDomain? medium = null,
        IEnumerable<AddressDomain>? addresses = null,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null,
        DomainStatus status = DomainStatus.Active
    )
        : base(id, createdAt, status, updatedAt, deletedAt)
    {
        Cpf = cpf;
        Name = name;
        Surname = surname;
        Emails = emails;
        Phones = phones;
        PersonOrixas = personOrixas;
        Manager = manager;
        Medium = medium;
        Addresses = addresses;
    }

    # endregion

    # region ---- factory ------------------------------------------------------

    public static PersonDomain Create(
        Cpf cpf,
        Name name,
        Name surname
    )
    {
        return new PersonDomain(
            id: Guid.Empty,
            createdAt: DateTime.UtcNow,
            cpf: cpf,
            name: name,
            surname: surname
        );
    }

    # endregion
}
