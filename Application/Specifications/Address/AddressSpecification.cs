using BrazilianTypes.Types;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Common.Types;

namespace Lira.Application.Specifications.Address;

public class AddressSpecification : ISpecification
{
    # region ---- properties ---------------------------------------------------

    private readonly string _street;
    private readonly string _number;
    private readonly string _neighborhood;
    private readonly string _city;
    private readonly string _state;
    private readonly string _zipCode;

    public StatusCode StatusCode { get; set; }
    public ICollection<string> ErrorMessages { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public AddressSpecification(
        string street,
        string number,
        string neighborhood,
        string city,
        string state,
        string zipCode
    )
    {
        _street = street;
        _number = number;
        _neighborhood = neighborhood;
        _city = city;
        _state = state;
        _zipCode = zipCode;
        StatusCode = StatusCode.Empty;
        ErrorMessages = new List<string>();
    }

    # endregion

    # region ---- specification ------------------------------------------------

    public bool IsSatisfiedBy()
    {
        var isSatisfiedBy = true;
        byte errors = 0;

        if (!Text.TryParse(_street, out _))
        {
            StatusCode = StatusCode.StreetIsInvalid;
            ErrorMessages.Add(item: AddressMessages.StreetIsInvalid);

            errors++;
            isSatisfiedBy = false;
        }

        if (!Text.TryParse(_number, out _))
        {
            StatusCode = StatusCode.StreetIsInvalid;
            ErrorMessages.Add(item: AddressMessages.StreetIsInvalid);

            errors++;
            isSatisfiedBy = false;
        }

        if (!Text.TryParse(_neighborhood, out _))
        {
            StatusCode = StatusCode.StreetIsInvalid;
            ErrorMessages.Add(item: AddressMessages.StreetIsInvalid);

            errors++;
            isSatisfiedBy = false;
        }

        if (!Text.TryParse(_city, out _))
        {
            StatusCode = StatusCode.StreetIsInvalid;
            ErrorMessages.Add(item: AddressMessages.StreetIsInvalid);

            errors++;
            isSatisfiedBy = false;
        }

        if (!ZipCode.TryParse(_zipCode, out _))
        {
            StatusCode = StatusCode.ZipCodeIsInvalid;
            ErrorMessages.Add(item: ZipCode.ErrorMessage);

            errors++;
            isSatisfiedBy = false;
        }

        if (!Uf.TryParse(_state, out _))
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
