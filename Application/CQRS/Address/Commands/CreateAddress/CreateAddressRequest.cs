using Lira.Application.Responses;
using MediatR;

namespace Lira.Application.CQRS.Address.Commands.CreateAddress;

public class CreateAddressRequest : IRequest<IHandlerResponse<CreateAddressResponse>>
{
    public string Street { get; init; }
    public string Number { get; init; }
    public string Neighborhood { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string ZipCode { get; init; }
    public string? Complement { get; init; }
    public Guid PersonId { get; init; }
    public bool ValidatePerson { get; init; }

    public CreateAddressRequest(
        string street,
        string number,
        string neighborhood,
        string city,
        string state,
        string zipCode,
        Guid personId,
        string? complement = null,
        bool validatePerson = true
    )
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        ZipCode = zipCode;
        Complement = complement;
        PersonId = personId;
        ValidatePerson = validatePerson;
    }
}
