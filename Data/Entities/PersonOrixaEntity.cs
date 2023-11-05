using Lira.Data.Enums;
using Lira.Domain.Domains.Orixa;
using Lira.Domain.Domains.Person;
using Lira.Domain.Domains.PersonOrixa;
using Lira.Domain.Enums;
using Lira.Domain.Religion.Enums;

namespace Lira.Data.Entities;

public class PersonOrixaEntity : BaseEntity
{
    public required OrixaType Type { get; set; }

    # region ---- relationship -------------------------------------------------

    public required Guid PersonId { get; set; }
    public PersonEntity? Person { get; set; }

    public required Guid OrixaId { get; set; }
    public OrixaEntity? Orixa { get; set; }

    # endregion

    # region ---- operators ----------------------------------------------------

    public static explicit operator PersonOrixaEntity(PersonOrixaDomain domain)
        => new()
        {
            Id = domain.Id,
            Type = domain.Type,
            PersonId = domain.PersonId,
            OrixaId = domain.OrixaId,
            CreatedAt = domain.CreatedAt,
            UpdatedAt = domain.UpdatedAt,
            DeletedAt = domain.DeletedAt,
            Status = (EntityStatus)domain.Status,
            Orixa = null,
            Person = null
        };

    public static explicit operator PersonOrixaDomain(PersonOrixaEntity entity)
    {
        if (entity.Person is not null)
        {
            entity.Person.PersonOrixas = null;
        }

        if (entity.Orixa is not null)
        {
            entity.Orixa.PersonOrixas = null;
        }

        return new PersonOrixaDomain(
            id: entity.Id,
            type: entity.Type,
            personId: entity.PersonId,
            orixaId: entity.OrixaId,
            createdAt: entity.CreatedAt,
            status: (DomainStatus)entity.Status,
            updatedAt: entity.UpdatedAt,
            deletedAt: entity.DeletedAt,
            orixa: entity.Orixa is null
                ? null
                : (OrixaDomain) entity.Orixa,
            person: entity.Person is null
                ? null
                : (PersonDomain) entity.Person
        );
    }

    # endregion
}
