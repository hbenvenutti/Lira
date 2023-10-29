using Lira.Application.Enums;
using Lira.Application.Errors;
using PasswordType = Lira.Common.Types.Password;

namespace Lira.Application.Specifications.Password;

public class PasswordSpecification
    : ISpecification
{
    # region ---- properties ---------------------------------------------------

    private readonly string _password;
    private readonly string _confirmation;
    public StatusCode StatusCode { get; set; }
    public ICollection<string> ErrorMessages { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public PasswordSpecification(
        string password,
        string confirmation
    )
    {
        _password = password;
        _confirmation = confirmation;
        ErrorMessages = new List<string>();
        StatusCode = StatusCode.Empty;
    }

    # endregion

    # region ---- satisfy ------------------------------------------------------

    public bool IsSatisfiedBy()
    {
        if (_password != _confirmation)
        {
            StatusCode = StatusCode.PasswordsDoNotMatch;
            ErrorMessages.Add(item: PasswordErrorMessages.PasswordsDoNotMatch);

            return false;
        }

        if (!PasswordType.TryParse(_password, out _))
        {
            StatusCode = StatusCode.PasswordIsInvalid;
            ErrorMessages.Add(item: PasswordErrorMessages.PasswordIsInvalid);

            return false;
        }

        return true;
    }

    # endregion
}
