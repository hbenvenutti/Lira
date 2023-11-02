using Lira.Application.Enums;
using Lira.Common.Types;

namespace Lira.Application.Specifications.Manager;

public class ManagerSpecification : ISpecification
{
    # region ---- properties ---------------------------------------------------

    public StatusCode StatusCode { get; set; }
    public ICollection<string> ErrorMessages { get; init; }

    private readonly string _username;

    # endregion

    # region ---- constructor --------------------------------------------------

    public ManagerSpecification(string username)
    {
        _username = username;
        StatusCode = StatusCode.Empty;
        ErrorMessages = new List<string>();
    }

    # endregion

    public bool IsSatisfiedBy()
    {
        if (!Username.TryParse(_username, out _))
        {
            StatusCode = StatusCode.UsernameIsInvalid;
            ErrorMessages.Add(Username.ErrorMessage);

            return false;
        }

        return true;
    }
}
