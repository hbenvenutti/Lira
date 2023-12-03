namespace Lira.Domain.Domains.Address;

public readonly struct AddressSpecificationDto
{
    # region ---- properties ---------------------------------------------------

    public string Street { get; init; }
    public string Number { get; init; }
    public string Neighborhood { get; init; }
    public string City { get; init; }
    public string ZipCode { get; init; }

    public string State { get; init; }

    # endregion

    # region ---- constructor --------------------------------------------------

    public AddressSpecificationDto(
        string street,
        string number,
        string neighborhood,
        string city,
        string zipCode,
        string state
    )
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        ZipCode = zipCode;
        State = state;
    }

    # endregion
}
