using Lira.Data.Enums;
using Lira.Domain.Domains.Orixa;
using Lira.Domain.Domains.PersonOrixa;
using Lira.Domain.Enums;

namespace Lira.Data.Entities;

public class OrixaEntity : BaseEntity
{
    # region ---- properties ---------------------------------------------------

    public required string Name { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public IEnumerable<PersonOrixaEntity>? PersonOrixas { get; set; }

    # endregion

    # region ---- operators ----------------------------------------------------

    public static explicit operator OrixaEntity(OrixaDomain domain)
        => new()
        {
            Id = domain.Id,
            Name = domain.Name,
            CreatedAt = domain.CreatedAt,
            UpdatedAt = domain.UpdatedAt,
            DeletedAt = domain.DeletedAt,
            Status = (EntityStatus)domain.Status,
            PersonOrixas = null
        };

    public static explicit operator OrixaDomain(OrixaEntity entity)
    {
        return new OrixaDomain(
            id: entity.Id,
            name: entity.Name,
            createdAt: entity.CreatedAt,
            status: (DomainStatus) entity.Status,
            updatedAt: entity.UpdatedAt,
            deletedAt: entity.DeletedAt,
            personOrixas: entity.PersonOrixas?
                .Select(personOrixa => (PersonOrixaDomain) personOrixa)
        );
    }

    # endregion
}
