using BrazilianTypes.Types;
using Lira.Data.Enums;
using Lira.Domain.Domains.Person;
using Lira.Domain.Domains.Phone;
using Lira.Domain.Enums;

namespace Lira.Data.Entities;

public class PhoneEntity : BaseEntity
{
    # region ---- properties ---------------------------------------------------

    public required string Number { get; set; }
    public required string Ddd { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public required Guid PersonId { get; set; }
    public PersonEntity? Person { get; set; }

    # endregion

    # region ---- operators ----------------------------------------------------

    public static explicit operator PhoneEntity(PhoneDomain phone)
        => new()
        {
            Id = phone.Id,
            CreatedAt = phone.CreatedAt,
            UpdatedAt = phone.UpdatedAt,
            DeletedAt = phone.DeletedAt,
            Status = (EntityStatus) phone.Status,
            Number = phone.Number,
            Ddd = phone.Ddd,
            PersonId = phone.PersonId,
            Person = null
        };

    public static explicit operator PhoneDomain(PhoneEntity phone)
    {
        if (phone.Person is not null)
        {
            phone.Person.Phones = null;
        }

        return new PhoneDomain(
            id: phone.Id,
            phone: Phone.FromSplit(ddd: phone.Ddd, number: phone.Number),
            status: (DomainStatus) phone.Status,
            createdAt: phone.CreatedAt,
            updatedAt: phone.UpdatedAt,
            personId: phone.PersonId,
            deletedAt: phone.DeletedAt,
            person: phone.Person is null
                ? null
                : (PersonDomain) phone.Person
        );
    }

    # endregion
}
