using Lira.Application.Enums;
using Lira.Common.Types;

namespace Lira.Application.Specifications.Manager;

public class ManagerSpecification : ISpecification<ManagerSpecificationDto>
{
    # region ---- properties ---------------------------------------------------

    public AppStatusCode AppStatusCode { get; set; } = AppStatusCode.Empty;
    public List<string> ErrorMessages { get; } = new List<string>();

    # endregion

    public bool IsSatisfiedBy(ManagerSpecificationDto data)
    {

        if (!Username.TryParse(data.Username, out _))
        {
            AppStatusCode = AppStatusCode.InvalidUsername;
            ErrorMessages.Add(Username.ErrorMessage);

            return false;
        }

        return true;
    }
}
