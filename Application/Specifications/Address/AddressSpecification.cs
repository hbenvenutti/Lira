// ReSharper disable ConvertConstructorToMemberInitializers

using BrazilianTypes.Types;
using Lira.Application.Enums;
using Lira.Application.Messages;

namespace Lira.Application.Specifications.Address;

public class AddressSpecification : ISpecification<AddressSpecificationDto>
{
    # region ---- properties ---------------------------------------------------

    public StatusCode StatusCode { get; set; } = StatusCode.Empty;
    public ICollection<string> ErrorMessages { get; init; }

    # endregion

    public AddressSpecification()
    {
        ErrorMessages = new List<string>();
    }

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
            StatusCode = StatusCode.AddressNumberIsInvalid;
            ErrorMessages.Add(item: AddressMessages.AddressNumberIsInvalid);

            errors++;
            isSatisfiedBy = false;
        }

        if (!Text.TryParse(data.Neighborhood, out _))
        {
            StatusCode = StatusCode.NeighborhoodIsInvalid;
            ErrorMessages.Add(item: AddressMessages.NeighborhoodIsInvalid);

            errors++;
            isSatisfiedBy = false;
        }

        if (!Text.TryParse(data.City, out _))
        {
            StatusCode = StatusCode.CityIsInvalid;
            ErrorMessages.Add(item: AddressMessages.CityIsInvalid);

            errors++;
            isSatisfiedBy = false;
        }

        if (!ZipCode.TryParse(data.ZipCode, out _))
        {
            StatusCode = StatusCode.ZipCodeIsInvalid;
            ErrorMessages.Add(item: AddressMessages.ZipCodeIsInvalid);

            errors++;
            isSatisfiedBy = false;
        }

        if (!Uf.TryParse(data.State, out _))
        {
            StatusCode = StatusCode.UfIsInvalid;
            ErrorMessages.Add(item: AddressMessages.StateIsInvalid);

            errors++;
            isSatisfiedBy = false;
        }

        if (errors > 1) { StatusCode = StatusCode.SeveralInvalidFields; }

        return isSatisfiedBy;
    }

    # endregion
}
