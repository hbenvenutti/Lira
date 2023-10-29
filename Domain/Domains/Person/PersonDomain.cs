using Lira.Common.Types;
using Lira.Domain.Domains.Base;
using Lira.Domain.Domains.Manager;
using Lira.Domain.Enums;

namespace Lira.Domain.Domains.Person;

public class PersonDomain : BaseDomain
{
    # region ---- properties ---------------------------------------------------

    public Cpf Cpf { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public ManagerDomain? Manager { get; set; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public PersonDomain(
        Guid id,
        DateTime createdAt,
        Cpf cpf,
        string name,
        string surname,
        ManagerDomain? manager = null,
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
    }

    # endregion
}
