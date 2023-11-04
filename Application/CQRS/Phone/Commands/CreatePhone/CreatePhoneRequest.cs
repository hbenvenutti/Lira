using Lira.Application.Responses;
using MediatR;

namespace Lira.Application.CQRS.Phone.Commands.CreatePhone;

public class CreatePhoneRequest : IRequest<Response<CreatePhoneResponse>>
{
    public Guid PersonId { get; init; }
    public string PhoneNumber { get; init; }

    public CreatePhoneRequest(
        string phoneNumber,
        Guid personId
    )
    {
        PhoneNumber = phoneNumber;
        PersonId = personId;
    }
}
