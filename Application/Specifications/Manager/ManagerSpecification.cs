using Lira.Application.Enums;
using Lira.Common.Types;

namespace Lira.Application.Specifications.Manager;

public class ManagerSpecification : ISpecification<ManagerSpecificationDto>
{
    # region ---- properties ---------------------------------------------------

    public StatusCode StatusCode { get; set; } = StatusCode.Empty;
    public ICollection<string> ErrorMessages { get; } = new List<string>();

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
