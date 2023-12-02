using Lira.Application.Responses;
using MediatR;

namespace Lira.Application.CQRS.Accounts.Commands.Login;

public class SignInRequest : IRequest<IHandlerResponse<SignInResponse>>
{
    public string Password { get; init; }
    public string Username { get; init; }

    public SignInRequest(string password, string username)
    {
        Password = password;
        Username = username;
    }
}
