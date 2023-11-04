using BrazilianTypes.Types;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Common.Types;

namespace Lira.Application.Specifications.Adress;

public class AddressSpecification : ISpecification<AddressSpecificationDto>
{
    # region ---- properties ---------------------------------------------------

    public StatusCode StatusCode { get; set; } = StatusCode.Empty;
    public ICollection<string> ErrorMessages { get; init; } = new List<string>();

    # endregion

    # region ---- specification ------------------------------------------------

    public bool IsSatisfiedBy(AddressSpecificationDto data)
    {
        var isSatisfiedBy = true;
        byte errors = 0;

        if (!Text.TryParse(data.Street, out _))
        {
            StatusCode = StatusCode.StreetIsInvalid;
            ErrorMessages.Add(item: AddressMessages.StreetIsInvalid);

            errors++;
            isSatisfiedBy = false;
        }

        if (!Text.TryParse(data.Number, out _))
        {
            StatusCode = StatusCode.StreetIsInvalid;
            ErrorMessages.Add(item: AddressMessages.StreetIsInvalid);

            errors++;
            isSatisfiedBy = false;
        }

        if (!Text.TryParse(data.Neighborhood, out _))
        {
            StatusCode = StatusCode.StreetIsInvalid;
            ErrorMessages.Add(item: AddressMessages.StreetIsInvalid);

            errors++;
            isSatisfiedBy = false;
        }

        if (!Text.TryParse(data.City, out _))
        {
            StatusCode = StatusCode.StreetIsInvalid;
            ErrorMessages.Add(item: AddressMessages.StreetIsInvalid);

            errors++;
            isSatisfiedBy = false;
        }

        if (!ZipCode.TryParse(data.ZipCode, out _))
        {
            StatusCode = StatusCode.ZipCodeIsInvalid;
            ErrorMessages.Add(item: ZipCode.ErrorMessage);

            errors++;
            isSatisfiedBy = false;
        }

        if (!Uf.TryParse(data.State, out _))
        {
            StatusCode = StatusCode.UfIsInvalid;
            ErrorMessages.Add(item: Uf.ErrorMessage);

            errors++;
            isSatisfiedBy = false;
        }

        if (errors > 1) { StatusCode = StatusCode.SeveralInvalidFields; }

        return isSatisfiedBy;
    }

    # endregion
}
