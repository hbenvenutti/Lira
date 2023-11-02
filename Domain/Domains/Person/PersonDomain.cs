using BrazilianTypes.Types;
using Lira.Common.Types;
using Lira.Domain.Domains.Address;
using Lira.Domain.Domains.Base;
using Lira.Domain.Domains.Manager;
using Lira.Domain.Domains.Medium;
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

    # endregion

    # region ---- constructor --------------------------------------------------

    public PersonDomain(
        Guid id,
        DateTime createdAt,
        Cpf cpf,
        Name name,
        Name surname,
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
