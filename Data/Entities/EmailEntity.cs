using Lira.Data.Enums;
using Lira.Domain.Domains;
using Lira.Domain.Domains.Person;
using Lira.Domain.Enums;

namespace Lira.Data.Entities;

public class EmailEntity : BaseEntity
{
    # region ---- properties --------------------------------------------------

    public required string Address { get; set; }
    public required EmailType Type { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public required Guid PersonId { get; set; }
    public PersonEntity? Person { get; set; }

    # endregion

    # region ---- operator -----------------------------------------------------

    public static explicit operator EmailEntity(EmailDomain domain) => new()
    {
        Id = domain.Id,
        Address = domain.Address,
        Type = domain.Type,
        PersonId = domain.PersonId,
        Person = null,
        CreatedAt = domain.CreatedAt,
        UpdatedAt = domain.UpdatedAt,
        DeletedAt = domain.DeletedAt,
        Status = (EntityStatus) domain.Status
    };

    public static explicit operator EmailDomain(EmailEntity entity)
    {
        if (entity.Person is not null)
        {
            entity.Person.Emails = null;
        }

        return new EmailDomain(
            id: entity.Id,
            address: entity.Address,
            type: entity.Type,
            createdAt: entity.CreatedAt,
            personId: entity.PersonId,
            person: entity.Person is null
                ? null
                : (PersonDomain) entity.Person,
            status: (DomainStatus) entity.Status,
            updatedAt: entity.UpdatedAt,
            deletedAt: entity.DeletedAt
        );
    }

    # endregion
}
