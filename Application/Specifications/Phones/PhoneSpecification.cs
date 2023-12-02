using BrazilianTypes.Types;
using Lira.Application.Messages;
using Lira.Common.Enums;
using Lira.Domain.Domains.Base;
using Lira.Domain.Domains.Person;

namespace Lira.Application.Specifications.Phones;

public class PhoneSpecification : ISpecification<PhoneSpecificationDto>
{
    # region ---- properties ---------------------------------------------------

    public AppStatusCode AppStatusCode { get; set; } = AppStatusCode.Empty;
    public List<string> ErrorMessages { get; } = new List<string>();

    # endregion

    # region ---- satisfy ------------------------------------------------------

    public bool IsSatisfiedBy(PhoneSpecificationDto data)
    {
        if (!Phone.TryParse(data.Phone, out _))
        {
            AppStatusCode = AppStatusCode.InvalidPhone;
            ErrorMessages.Add(PersonMessages.InvalidPhone);

            return false;
        }

        return true;
    }

    # endregion
}
