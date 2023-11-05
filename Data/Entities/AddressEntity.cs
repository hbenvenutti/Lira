using Lira.Data.Enums;
using Lira.Domain.Domains.Address;
using Lira.Domain.Domains.Person;
using Lira.Domain.Enums;

namespace Lira.Data.Entities;

public class AddressEntity : BaseEntity
{
    # region ---- properties ---------------------------------------------------

    public required string Street { get; set; }
    public required string Number { get; set; }
    public required string Neighborhood { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
    public string? Complement { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public Guid PersonId { get; set; }
    public PersonEntity? Person { get; set; }

    # endregion

    # region ---- operators ----------------------------------------------------

    public static explicit operator AddressEntity(AddressDomain domain) =>
        new()
        {
            Id = domain.Id,
            Status = (EntityStatus)domain.Status,
            CreatedAt = domain.CreatedAt,
            DeletedAt = domain.DeletedAt,
            UpdatedAt = domain.UpdatedAt,
            Street = domain.Street,
            Number = domain.Number,
            Neighborhood = domain.Neighborhood,
            City = domain.City,
            State = domain.State,
            ZipCode = domain.ZipCode,
            Complement = domain.Complement,
            PersonId = domain.PersonId,
            Person = null
        };

    public static explicit operator AddressDomain(AddressEntity entity)
    {
        if (entity.Person is not null)
        {
            entity.Person.Addresses = null;
        }

        return new AddressDomain(
            id: entity.Id,
            street: entity.Street,
            number: entity.Number,
            neighborhood: entity.Neighborhood,
            city: entity.City,
            state: entity.State,
            zipCode: entity.ZipCode,
            personId: entity.PersonId,
            createdAt: entity.CreatedAt,
            status: (DomainStatus) entity.Status,
            complement: entity.Complement,
            updatedAt: entity.UpdatedAt,
            deletedAt: entity.DeletedAt,

            person: entity.Person is null
                ? null
                : (PersonDomain) entity.Person
        );
    }

    # endregion
}
