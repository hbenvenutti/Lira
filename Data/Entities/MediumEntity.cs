using Lira.Data.Enums;
using Lira.Domain.Domains.Medium;
using Lira.Domain.Domains.Person;
using Lira.Domain.Enums;

namespace Lira.Data.Entities;

public class MediumEntity : BaseEntity
{
    # region ---- properties ---------------------------------------------------

    public required DateTime? FirstAmaci { get; set; }
    public required DateTime? LastAmaci { get; set; }

    # endregion

    # region ---- relationships ------------------------------------------------

    public required Guid PersonId { get; set; }
    public PersonEntity? Person { get; set; }

    # endregion

    # region ---- operators ----------------------------------------------------

    public static explicit operator MediumEntity(MediumDomain domain)
    {
        return new MediumEntity
        {
            Id = domain.Id,
            Status = (EntityStatus) domain.Status,
            CreatedAt = domain.CreatedAt,
            UpdatedAt = domain.UpdatedAt,
            DeletedAt = domain.DeletedAt,
            FirstAmaci = domain.FirstAmaci,
            LastAmaci = domain.LastAmaci,
            PersonId = domain.PersonId,
            Person = null
        };
    }

    public static explicit operator MediumDomain(MediumEntity entity)
    {
        if (entity.Person is not null)
        {
            entity.Person.Medium = null;
        }

        return new MediumDomain(
            id: entity.Id,
            status: (DomainStatus) entity.Status,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt,
            deletedAt: entity.DeletedAt,
            firstAmaci: entity.FirstAmaci,
            lastAmaci: entity.LastAmaci,
            personId: entity.PersonId,
            person: entity.Person is null
                ? null
                : (PersonDomain) entity.Person
        );
    }

    # endregion
}
