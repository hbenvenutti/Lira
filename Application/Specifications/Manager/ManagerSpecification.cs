using Lira.Application.Enums;
using Lira.Common.Types;

namespace Lira.Application.Specifications.Manager;

public class ManagerSpecification : ISpecification<ManagerSpecificationDto>
{
    # region ---- properties ---------------------------------------------------

    public StatusCode StatusCode { get; set; }
    public ICollection<string> ErrorMessages { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public ManagerSpecification(string username)
    {
        StatusCode = StatusCode.Empty;
        ErrorMessages = new List<string>();
    }

    # endregion

    public bool IsSatisfiedBy(ManagerSpecificationDto data)
    {

        if (!Username.TryParse(data.Username, out _))
        {
            StatusCode = StatusCode.UsernameIsInvalid;
            ErrorMessages.Add(Username.ErrorMessage);

            return false;
        }

        return true;
    }
}
