using BrazilianTypes.Types;
using Lira.Application.Enums;
using Lira.Application.Messages;

namespace Lira.Application.Specifications.Phones;

public class PhoneSpecification : ISpecification<PhoneSpecificationDto>
{
    # region ---- properties ---------------------------------------------------

    public StatusCode StatusCode { get; set; } = StatusCode.Empty;
    public ICollection<string> ErrorMessages { get; } = new List<string>();

    # endregion

    # region ---- satisfy ------------------------------------------------------

    public bool IsSatisfiedBy(PhoneSpecificationDto data)
    {
        if (!Phone.TryParse(data.Phone, out _))
        {
            StatusCode = StatusCode.PhoneIsInvalid;
            ErrorMessages.Add(PersonMessages.InvalidPhone);

            return false;
        }

        return true;
    }

    # endregion
}
