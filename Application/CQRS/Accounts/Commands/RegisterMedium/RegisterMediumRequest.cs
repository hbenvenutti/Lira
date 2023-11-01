using Lira.Application.Responses;
using MediatR;

namespace Lira.Application.CQRS.Accounts.Commands.RegisterMedium;

public class RegisterMediumRequest : IRequest<Response<RegisterMediumResponse>>
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

    public DateTime? FirstAmaci { get; init; }
    public DateTime? LastAmaci { get; init; }

    public Guid? FrontOrixaId { get; init; }
    public Guid? CloseOrixaId { get; init; }
    public Guid? AncestralOrixaId { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public RegisterMediumRequest(
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
        Guid? closeOrixaId,
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
        FirstAmaci = firstAmaci;
        LastAmaci = lastAmaci;
        FrontOrixaId = frontOrixaId;
        CloseOrixaId = closeOrixaId;
        AncestralOrixaId = ancestralOrixaId;
    }

    # endregion
}
