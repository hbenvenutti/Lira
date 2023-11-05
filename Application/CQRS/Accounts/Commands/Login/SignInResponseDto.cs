namespace Lira.Application.CQRS.Accounts.Commands.Login;

public class SignInResponseDto
{
    # region ---- properties ---------------------------------------------------

    public string Token { get; init; }

    # endregion

    public SignInResponseDto(string token)
    {
        Token = token;
    }
}
