using BrazilianTypes.Types;
using Lira.Application.Enums;

namespace Lira.Application.Specifications;

public class PhoneSpecification : ISpecification
{
    # region ---- properties ---------------------------------------------------

    private readonly string _phone;
    public StatusCode StatusCode { get; set; }
    public ICollection<string> ErrorMessages { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public PhoneSpecification(string phone)
    {
        _phone = phone;
        StatusCode = StatusCode.Empty;
        ErrorMessages = new List<string>();
    }

    # endregion

    # region ---- satisfy ------------------------------------------------------

    public bool IsSatisfiedBy()
    {
        if (!Phone.TryParse(_phone, out _))
        {
            StatusCode = StatusCode.PhoneIsInvalid;
            ErrorMessages.Add(Phone.ErrorMessage);

            return false;
        }

        return true;
    }

    # endregion
}
