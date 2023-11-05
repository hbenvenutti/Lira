using Lira.Application.Responses;
using MediatR;

namespace Lira.Application.CQRS.Managers.Commands.CreateManager;

public class CreateManagerRequest : IRequest<Response<CreateManagerResponse>>
{
    public Guid PersonId { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    public string Confirmation { get; init; }

    public CreateManagerRequest(
        Guid personId,
        string username,
        string password,
        string confirmation
    )
    {
        Username = username;
        Password = password;
        Confirmation = confirmation;
        PersonId = personId;
    }
}
