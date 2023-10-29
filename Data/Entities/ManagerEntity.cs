using Lira.Data.Enums;
using Lira.Domain.Domains.Manager;
using Lira.Domain.Domains.Person;
using Lira.Domain.Enums;

namespace Lira.Data.Entities;

public class ManagerEntity : BaseEntity
{
    # region ---- properties ---------------------------------------------------

    public required string Username { get; set; }
    public required string Password { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public required Guid PersonId { get; set; }
    public PersonEntity? Person { get; set; }

    # endregion

    # region ---- operators ----------------------------------------------------

    public static explicit operator ManagerEntity(ManagerDomain domain) =>
        new()
        {
            Id = domain.Id,
            Status = (EntityStatus)domain.Status,
            CreatedAt = domain.CreatedAt,
            DeletedAt = domain.DeletedAt,
            UpdatedAt = domain.UpdatedAt,
            Username = domain.Username,
            Password = domain.Password,
            PersonId = domain.PersonId,
            Person = null
        };

    public static explicit operator ManagerDomain(ManagerEntity entity)
    {
        if (entity.Person is not null)
        {
            entity.Person.Manager = null;
        }

        return new ManagerDomain(
            id: entity.Id,
            createdAt: entity.CreatedAt,
            username: entity.Username,
            password: entity.Password,
            personId: entity.PersonId,
            updatedAt: entity.UpdatedAt,
            deletedAt: entity.DeletedAt,
            status: (DomainStatus)entity.Status,
            person: entity.Person is null
                ? null
                : (PersonDomain)entity.Person
        );
    }

    # endregion
}
