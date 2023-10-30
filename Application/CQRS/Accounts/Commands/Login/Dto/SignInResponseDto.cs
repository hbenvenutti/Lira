namespace Lira.Application.CQRS.Accounts.Commands.Login.Dto;

public readonly struct SignInResponseDto
{
    # region ---- properties ---------------------------------------------------

    public string Token { get; init; }

    # endregion

    public SignInResponseDto(string token)
    {
        Token = token;
    }
}
