using Lira.Application.Responses;
using MediatR;

namespace Lira.Application.CQRS.Managers.Commands.CreateAdmin;

public class CreateAdminRequest : IRequest<Response<CreateAdminResponseDto>>
{
    # region ---- properties ---------------------------------------------------

    public string Code { get; init; }
    public string Password { get; init; }
    public string PasswordConfirmation { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Cpf { get; init; }
    public string Username { get; init; }

    # endregion

    # region ---- constructors -------------------------------------------------

    public CreateAdminRequest(
        string code,
        string password,
        string passwordConfirmation,
        string name,
        string surname,
        string cpf,
        string username
    )
    {
        Code = code;
        Password = password;
        PasswordConfirmation = passwordConfirmation;
        Name = name;
        Surname = surname;
        Cpf = cpf;
        Username = username;
    }

    # endregion
}
