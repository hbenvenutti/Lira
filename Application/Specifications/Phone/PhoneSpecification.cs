using Lira.Application.Enums;
using PhoneType = BrazilianTypes.Types.Phone;
namespace Lira.Application.Specifications.Phone;

public class PhoneSpecification : ISpecification<PhoneSpecificationDto>
{
    # region ---- properties ---------------------------------------------------

    public StatusCode StatusCode { get; set; } = StatusCode.Empty;
    public ICollection<string> ErrorMessages { get; init; } = new List<string>();

    # endregion

    # region ---- satisfy ------------------------------------------------------

    public bool IsSatisfiedBy(PhoneSpecificationDto data)
    {
        if (!PhoneType.TryParse(data.Phone, out _))
        {
            StatusCode = StatusCode.PhoneIsInvalid;
            ErrorMessages.Add(PhoneType.ErrorMessage);

            return false;
        }

        return true;
    }

    # endregion
}
