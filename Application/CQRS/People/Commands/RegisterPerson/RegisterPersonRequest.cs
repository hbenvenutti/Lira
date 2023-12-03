using Lira.Application.Responses;
using Lira.Domain.Enums;
using MediatR;

namespace Lira.Application.CQRS.People.Commands.RegisterPerson;

public class RegisterPersonRequest : IRequest<HandlerResponse<RegisterPersonResponse>>
{
    # region ---- properties ---------------------------------------------------

    public string FirstName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public EmailType? EmailType { get; init; }
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

    public RegisterPersonRequest(
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
        DateTime? firstAmaci,
        DateTime? lastAmaci,
        Guid? frontOrixaId,
        Guid? adjunctOrixaId,
        Guid? ancestralOrixaId,
        EmailType? emailType,
        bool isMedium = false
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
        FirstAmaci = firstAmaci;
        LastAmaci = lastAmaci;
        FrontOrixaId = frontOrixaId;
        AdjunctOrixaId = adjunctOrixaId;
        AncestralOrixaId = ancestralOrixaId;
        EmailType = emailType;
        IsMedium = isMedium;
    }

    # endregion
}
