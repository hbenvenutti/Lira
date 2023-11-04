using Lira.Application.Enums;
using Lira.Application.Messages;

namespace Lira.Application.Specifications.Password;

public class PasswordSpecification : ISpecification<PasswordSpecificationDto>
{
    # region ---- properties ---------------------------------------------------
    public StatusCode StatusCode { get; set; } = StatusCode.Empty;
    public ICollection<string> ErrorMessages { get; init; } = new List<string>();

    # endregion

    # region ---- satisfy ------------------------------------------------------

    public bool IsSatisfiedBy(PasswordSpecificationDto data)
    {
        if (data.Password != data.Confirmation)
        {
            StatusCode = StatusCode.PasswordsDoNotMatch;
            ErrorMessages.Add(item: AccountsMessages.PasswordsDoNotMatch);

            return false;
        }

        if (!Common.Types.Password.TryParse(data.Password, out _))
        {
            StatusCode = StatusCode.PasswordIsInvalid;
            ErrorMessages.Add(item: AccountsMessages.PasswordIsInvalid);

            return false;
        }

        return true;
    }

    # endregion
}
