using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Common.Types;

namespace Lira.Application.Specifications.Passwords;

public class PasswordSpecification : ISpecification<PasswordSpecificationDto>
{
    # region ---- properties ---------------------------------------------------
    public AppStatusCode AppStatusCode { get; set; } = AppStatusCode.Empty;
    public List<string> ErrorMessages { get; } = new List<string>();

    # endregion

    # region ---- satisfy ------------------------------------------------------

    public bool IsSatisfiedBy(PasswordSpecificationDto data)
    {
        if (data.Password != data.Confirmation)
        {
            AppStatusCode = AppStatusCode.PasswordsDoNotMatch;
            ErrorMessages.Add(item: ManagerMessages.PasswordsDoNotMatch);

            return false;
        }

        if (!Password.TryParse(data.Password, out _))
        {
            AppStatusCode = AppStatusCode.InvalidPassword;
            ErrorMessages.Add(item: ManagerMessages.InvalidPassword);

            return false;
        }

        return true;
    }

    # endregion
}
