using BrazilianTypes.Types;
using Lira.Common.Enums;
using Lira.Domain.Domains.Base;

namespace Lira.Domain.Domains.Address;

public class AddressSpecification : ISpecification<AddressSpecificationDto>
{
    # region ---- properties ---------------------------------------------------

    public AppStatusCode AppStatusCode { get; set; } = AppStatusCode.Empty;
    public List<string> ErrorMessages { get; } = new();

    # endregion

    # region ---- specification ------------------------------------------------

    public bool IsSatisfiedBy(AddressSpecificationDto data)
    {
        byte errors = 0;

        if (!Text.TryParse(data.Street, out _))
        {
            AppStatusCode = AppStatusCode.InvalidStreet;
            ErrorMessages.Add(item: AddressMessages.StreetIsInvalid);

            errors++;
        }

        if (!Text.TryParse(data.Number, out _))
        {
            AppStatusCode = AppStatusCode.InvalidAddressNumber;
            ErrorMessages.Add(item: AddressMessages.AddressNumberIsInvalid);

            errors++;
        }

        if (!Text.TryParse(data.Neighborhood, out _))
        {
            AppStatusCode = AppStatusCode.InvalidNeighborhood;
            ErrorMessages.Add(item: AddressMessages.NeighborhoodIsInvalid);

            errors++;
        }

        if (!Text.TryParse(data.City, out _))
        {
            AppStatusCode = AppStatusCode.InvalidCity;
            ErrorMessages.Add(item: AddressMessages.CityIsInvalid);

            errors++;
        }

        if (!ZipCode.TryParse(data.ZipCode, out _))
        {
            AppStatusCode = AppStatusCode.InvalidZipCode;
            ErrorMessages.Add(item: AddressMessages.ZipCodeIsInvalid);

            errors++;
        }

        if (!Uf.TryParse(data.State, out _))
        {
            AppStatusCode = AppStatusCode.InvalidUf;
            ErrorMessages.Add(item: AddressMessages.StateIsInvalid);

            errors++;
        }

        if (errors > 1) { AppStatusCode = AppStatusCode.SeveralInvalidFields; }

        return errors == 0;
    }

    # endregion
}
