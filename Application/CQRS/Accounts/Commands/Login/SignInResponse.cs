namespace Lira.Application.CQRS.Accounts.Commands.Login;

public class SignInResponse
{
    # region ---- properties ---------------------------------------------------

    public string Token { get; init; }

    # endregion

    public SignInResponse(string token)
    {
        Token = token;
    }
}
