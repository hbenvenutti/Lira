using Lira.Application.CQRS.People.Commands.RegisterPerson;

namespace Lira.Api.Controllers.People.Dto;

public class RegisterPersonBodyDto
{
    # region ---- properties ---------------------------------------------------

    public string FirstName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string Document { get; init; }

    public string Street { get; init; }
    public string Number { get; init; }
    public string Neighborhood { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }
    public string? Complement { get; init; }

    public bool IsMedium { get; init; }
    public DateTime? FirstAmaci { get; init; }
    public DateTime? LastAmaci { get; init; }

    public Guid? FrontOrixaId { get; init; }
    public Guid? AdjunctOrixaId { get; init; }
    public Guid? AncestralOrixaId { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public RegisterPersonBodyDto(
        string firstName,
        string surname,
        string email,
        string phoneNumber,
        string document,
        string street,
        string number,
        string neighborhood,
        string city,
        string state,
        string zipCode,
        string? complement,
        bool isMedium,
        DateTime? firstAmaci,
        DateTime? lastAmaci,
        Guid? frontOrixaId,
        Guid? adjunctOrixaId,
        Guid? ancestralOrixaId
    )
    {
        FirstName = firstName;
        Surname = surname;
        Email = email;
        PhoneNumber = phoneNumber;
        Document = document;
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        ZipCode = zipCode;
        Complement = complement;
        IsMedium = isMedium;
        FirstAmaci = firstAmaci;
        LastAmaci = lastAmaci;
        FrontOrixaId = frontOrixaId;
        AdjunctOrixaId = adjunctOrixaId;
        AncestralOrixaId = ancestralOrixaId;
    }

    # endregion

    # region ---- operators ----------------------------------------------------

    public static implicit operator RegisterPersonRequest(
        RegisterPersonBodyDto dto
    ) => new(
        firstName: dto.FirstName,
        surname: dto.Surname,
        email: dto.Email,
        phoneNumber: dto.PhoneNumber,
        document: dto.Document,
        street: dto.Street,
        number: dto.Number,
        neighborhood: dto.Neighborhood,
        city: dto.City,
        state: dto.State,
        zipCode: dto.ZipCode,
        complement: dto.Complement,
        isMedium: dto.IsMedium,
        firstAmaci: dto.FirstAmaci,
        lastAmaci: dto.LastAmaci,
        frontOrixaId: dto.FrontOrixaId,
        adjunctOrixaId: dto.AdjunctOrixaId,
        ancestralOrixaId: dto.AncestralOrixaId
    );

    # endregion
}
