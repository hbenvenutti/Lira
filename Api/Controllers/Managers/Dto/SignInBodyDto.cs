using Lira.Application.CQRS.Accounts.Commands.Login;

namespace Lira.Api.Controllers.Managers.Dto;

public struct SignInBodyDto
{
    public string Password { get; init; }
    public string Username { get; init; }

    public SignInBodyDto(string password, string username)
    {
        Password = password;
        Username = username;
    }

    public static implicit operator SignInRequest(SignInBodyDto body)
        => new(
            password: body.Password,
            username: body.Username
        );
}
