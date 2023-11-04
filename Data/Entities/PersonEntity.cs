using Lira.Data.Enums;
using Lira.Domain.Domains.Address;
using Lira.Domain.Domains.Emails;
using Lira.Domain.Domains.Manager;
using Lira.Domain.Domains.Medium;
using Lira.Domain.Domains.Person;
using Lira.Domain.Domains.PersonOrixa;
using Lira.Domain.Domains.Phone;
using Lira.Domain.Enums;

namespace Lira.Data.Entities;

public class PersonEntity : BaseEntity
{
    # region ---- properties ---------------------------------------------------

    public required string Cpf { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }

    # endregion

    # region ---- relations ----------------------------------------------------

    public ManagerEntity? Manager { get; set; }
    public MediumEntity? Medium { get; set; }

    public IEnumerable<EmailEntity>? Emails { get; set; }
    public IEnumerable<PhoneEntity>? Phones { get; set; }
    public IEnumerable<AddressEntity>? Addresses { get; set; }
    public IEnumerable<PersonOrixaEntity>? PersonOrixas { get; set; }

    # endregion

    # region ---- operators ----------------------------------------------------

    public static explicit operator PersonEntity(PersonDomain domain) =>
        new()
        {
            Id = domain.Id,
            Status = (EntityStatus)domain.Status,
            CreatedAt = domain.CreatedAt,
            DeletedAt = domain.DeletedAt,
            UpdatedAt = domain.UpdatedAt,
            Cpf = domain.Cpf,
            Name = domain.Name,
            Surname = domain.Surname,
            Manager = null,
            Emails = null,
            Phones = null,
            Addresses = null,
            Medium = null,
            PersonOrixas = null
        };

    public static explicit operator PersonDomain(PersonEntity entity)
    {
        if (entity.Manager is not null)
        {
            entity.Manager.Person = null;
        }

        if (entity.Medium is not null)
        {
            entity.Medium.Person = null;
        }

        return new PersonDomain(
            id: entity.Id,
            createdAt: entity.CreatedAt,
            cpf: entity.Cpf,
            name: entity.Name,
            surname: entity.Surname,
            updatedAt: entity.UpdatedAt,
            deletedAt: entity.DeletedAt,
            status: (DomainStatus) entity.Status,

            manager : entity.Manager is null
                ? null
                : (ManagerDomain) entity.Manager,

            medium : entity.Medium is null
                ? null
                : (MediumDomain) entity.Medium,

            addresses: entity.Addresses?
                .Select(address => (AddressDomain) address),

            phones: entity.Phones?
                .Select(phone => (PhoneDomain) phone),

            personOrixas: entity.PersonOrixas?
                .Select(personOrixa => (PersonOrixaDomain) personOrixa),

            emails: entity.Emails?
                .Select(email => (EmailDomain) email)
        );
    }

    # endregion
}
