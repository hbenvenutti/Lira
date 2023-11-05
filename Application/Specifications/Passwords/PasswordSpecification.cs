using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Common.Types;

namespace Lira.Application.Specifications.Passwords;

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
            ErrorMessages.Add(item: ManagerMessages.PasswordsDoNotMatch);

            return false;
        }

        if (!Password.TryParse(data.Password, out _))
        {
            StatusCode = StatusCode.PasswordIsInvalid;
            ErrorMessages.Add(item: ManagerMessages.PasswordIsInvalid);

            return false;
        }

        return true;
    }

    # endregion
}
