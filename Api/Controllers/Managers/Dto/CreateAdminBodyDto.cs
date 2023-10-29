using Lira.Application.CQRS.Managers.Commands.CreateAdmin;

namespace Lira.Api.Controllers.Managers.Dto;

public readonly struct CreateAdminBodyDto
{
    # region ---- properties ---------------------------------------------------

    public string Code { get; init; }
    public string Password { get; init; }
    public string PasswordConfirmation { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Cpf { get; init; }

    # endregion

    # region ---- operators ----------------------------------------------------

    public static implicit operator CreateAdminRequest(CreateAdminBodyDto body)
        => new(
            code: body.Code,
            password: body.Password,
            passwordConfirmation: body.PasswordConfirmation,
            name: body.Name,
            surname: body.Surname,
            cpf: body.Cpf
        );

    # endregion
}
