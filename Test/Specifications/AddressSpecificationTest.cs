using System.Diagnostics.CodeAnalysis;
using Lira.Common.Enums;
using Lira.Domain.Domains.Address;

namespace Lira.Test.Specifications;

[ExcludeFromCodeCoverage]
public class AddressSpecificationTest
{
    private readonly AddressSpecification _addressSpecification = new();

    private const string Street = "Street";
    private const string Number = "100";
    private const string Neighborhood = "Downtown";
    private const string City = "São Paulo";
    private const string ZipCode = "99999-999";
    private const string State = "SP";

    private const byte Invalid = 0;
    private const byte Valid = 1;

    # region ---- IsSatisfiedBy -------------------------------------------------

    [Fact]
    public void ShouldBeSatisfiedBy()
    {
        var addressSpecificationDto = new AddressSpecificationDto(
            street: Street,
            number: Number,
            neighborhood: Neighborhood,
            city: City,
            state: State,
            zipCode: ZipCode
        );

        var isSatisfiedBy = _addressSpecification.IsSatisfiedBy(
            data: addressSpecificationDto
        );

        Assert.True(isSatisfiedBy);
        Assert.Equal(
            expected: AppStatusCode.Empty,
            actual: _addressSpecification.AppStatusCode
        );
        Assert.IsType<List<string>>(_addressSpecification.ErrorMessages);
        Assert.Empty(_addressSpecification.ErrorMessages);
    }

    # endregion

    # region ---- Street -------------------------------------------------------

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ShouldNotBeSatisfiedByStreetIsInvalid(
        string street
    )
    {
        var addressSpecificationDto = new AddressSpecificationDto(
            street: street,
            number: Number,
            neighborhood: Neighborhood,
            city: City,
            state: State,
            zipCode: ZipCode
        );

        var isSatisfiedBy = _addressSpecification.IsSatisfiedBy(
            data: addressSpecificationDto
        );

        Assert.False(isSatisfiedBy);

        Assert.Equal(
            expected: AppStatusCode.InvalidStreet,
            actual: _addressSpecification.AppStatusCode
        );

        Assert.Single(_addressSpecification.ErrorMessages);

        Assert.Contains(
            expected: AddressMessages.StreetIsInvalid,
            collection: _addressSpecification.ErrorMessages
        );
    }

    # endregion

    # region ---- Number -------------------------------------------------------

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ShouldNotBeSatisfiedByNumberIsInvalid(
        string number
    )
    {
        var addressSpecificationDto = new AddressSpecificationDto(
            street: Street,
            number: number,
            neighborhood: Neighborhood,
            city: City,
            state: State,
            zipCode: ZipCode
        );

        var isSatisfiedBy = _addressSpecification.IsSatisfiedBy(
            data: addressSpecificationDto
        );

        Assert.False(isSatisfiedBy);

        Assert.Equal(
            expected: AppStatusCode.InvalidAddressNumber,
            actual: _addressSpecification.AppStatusCode
        );

        Assert.Single(_addressSpecification.ErrorMessages);

        Assert.Contains(
            expected: AddressMessages.AddressNumberIsInvalid,
            collection: _addressSpecification.ErrorMessages
        );
    }

    # endregion

    # region ---- Neighborhood -------------------------------------------------

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ShouldNotBeSatisfiedByNeighborhoodIsInvalid(
        string neighborhood
    )
    {
        var addressSpecificationDto = new AddressSpecificationDto(
            street: Street,
            number: Number,
            neighborhood: neighborhood,
            city: City,
            state: State,
            zipCode: ZipCode
        );

        var isSatisfiedBy = _addressSpecification.IsSatisfiedBy(
            data: addressSpecificationDto
        );

        Assert.False(isSatisfiedBy);

        Assert.Equal(
            expected: AppStatusCode.InvalidNeighborhood,
            actual: _addressSpecification.AppStatusCode
        );

        Assert.Single(_addressSpecification.ErrorMessages);

        Assert.Contains(
            expected: AddressMessages.NeighborhoodIsInvalid,
            collection: _addressSpecification.ErrorMessages
        );
    }

    # endregion

    # region ---- City ---------------------------------------------------------

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void ShouldNotBeSatisfiedByCityIsInvalid(
        string city
    )
    {
        var addressSpecificationDto = new AddressSpecificationDto(
            street: Street,
            number: Number,
            neighborhood: Neighborhood,
            city: city,
            state: State,
            zipCode: ZipCode
        );

        var isSatisfiedBy = _addressSpecification.IsSatisfiedBy(
            data: addressSpecificationDto
        );

        Assert.False(isSatisfiedBy);

        Assert.Equal(
            expected: AppStatusCode.InvalidCity,
            actual: _addressSpecification.AppStatusCode
        );

        Assert.Single(_addressSpecification.ErrorMessages);

        Assert.Contains(
            expected: AddressMessages.CityIsInvalid,
            collection: _addressSpecification.ErrorMessages
        );
    }

    # endregion

    # region ---- ZipCode ------------------------------------------------------

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    // [InlineData(null)] todo: fix in the lib to handle null
    [InlineData("99999-99")]
    [InlineData("AAbbA-BBB")]
    public void ShouldNotBeSatisfiedByZipCodeIsInvalid(
        string zipCode
    )
    {
        var addressSpecificationDto = new AddressSpecificationDto(
            street: Street,
            number: Number,
            neighborhood: Neighborhood,
            city: City,
            state: State,
            zipCode: zipCode
        );

        var isSatisfiedBy = _addressSpecification.IsSatisfiedBy(
            data: addressSpecificationDto
        );

        Assert.False(isSatisfiedBy);

        Assert.Equal(
            expected: AppStatusCode.InvalidZipCode,
            actual: _addressSpecification.AppStatusCode
        );

        Assert.Single(_addressSpecification.ErrorMessages);

        Assert.Contains(
            expected: AddressMessages.ZipCodeIsInvalid,
            collection: _addressSpecification.ErrorMessages
        );
    }

    # endregion

    # region ---- State --------------------------------------------------------

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("São Paulo")]
    [InlineData("SPA")]
    public void ShouldNotBeSatisfiedByStateIsInvalid(
        string state
    )
    {
        var addressSpecificationDto = new AddressSpecificationDto(
            street: Street,
            number: Number,
            neighborhood: Neighborhood,
            city: City,
            state: state,
            zipCode: ZipCode
        );

        var isSatisfiedBy = _addressSpecification.IsSatisfiedBy(
            data: addressSpecificationDto
        );

        Assert.False(isSatisfiedBy);

        Assert.Equal(
            expected: AppStatusCode.InvalidUf,
            actual: _addressSpecification.AppStatusCode
        );

        Assert.Single(_addressSpecification.ErrorMessages);

        Assert.Contains(
            expected: AddressMessages.StateIsInvalid,
            collection: _addressSpecification.ErrorMessages
        );
    }

    # endregion

    # region ---- SeveralInvalidFields -----------------------------------------

    [Theory]
    [InlineData(Invalid, Invalid, Invalid, Invalid, Invalid, Invalid)]
    [InlineData(Invalid, Invalid, Invalid, Invalid, Invalid, Valid)]
    [InlineData(Invalid, Invalid, Invalid, Invalid, Valid, Invalid)]
    [InlineData(Invalid, Invalid, Invalid, Invalid, Valid, Valid)]
    [InlineData(Invalid, Invalid, Invalid, Valid, Invalid, Invalid)]
    [InlineData(Invalid, Invalid, Invalid, Valid, Invalid, Valid)]
    [InlineData(Invalid, Invalid, Invalid, Valid, Valid, Invalid)]
    [InlineData(Invalid, Invalid, Invalid, Valid, Valid, Valid)]
    [InlineData(Invalid, Invalid, Valid, Invalid, Invalid, Invalid)]
    [InlineData(Invalid, Invalid, Valid, Invalid, Invalid, Valid)]
    [InlineData(Invalid, Invalid, Valid, Invalid, Valid, Invalid)]
    [InlineData(Invalid, Invalid, Valid, Invalid, Valid, Valid)]
    [InlineData(Invalid, Invalid, Valid, Valid, Invalid, Invalid)]
    [InlineData(Invalid, Invalid, Valid, Valid, Invalid, Valid)]
    [InlineData(Invalid, Invalid, Valid, Valid, Valid, Invalid)]
    [InlineData(Invalid, Invalid, Valid, Valid, Valid, Valid)]
    [InlineData(Invalid, Valid, Invalid, Invalid, Invalid, Invalid)]
    [InlineData(Invalid, Valid, Invalid, Invalid, Invalid, Valid)]
    [InlineData(Invalid, Valid, Invalid, Invalid, Valid, Invalid)]
    [InlineData(Invalid, Valid, Invalid, Invalid, Valid, Valid)]
    [InlineData(Invalid, Valid, Invalid, Valid, Invalid, Invalid)]
    [InlineData(Invalid, Valid, Invalid, Valid, Invalid, Valid)]
    [InlineData(Invalid, Valid, Invalid, Valid, Valid, Invalid)]
    [InlineData(Invalid, Valid, Invalid, Valid, Valid, Valid)]
    [InlineData(Invalid, Valid, Valid, Invalid, Invalid, Invalid)]
    [InlineData(Invalid, Valid, Valid, Invalid, Invalid, Valid)]
    [InlineData(Invalid, Valid, Valid, Invalid, Valid, Invalid)]
    [InlineData(Invalid, Valid, Valid, Invalid, Valid, Valid)]
    [InlineData(Invalid, Valid, Valid, Valid, Invalid, Invalid)]
    [InlineData(Invalid, Valid, Valid, Valid, Invalid, Valid)]
    [InlineData(Invalid, Valid, Valid, Valid, Valid, Invalid)]
    [InlineData(Valid, Invalid, Invalid, Invalid, Invalid, Invalid)]
    [InlineData(Valid, Invalid, Invalid, Invalid, Invalid, Valid)]
    [InlineData(Valid, Invalid, Invalid, Invalid, Valid, Invalid)]
    [InlineData(Valid, Invalid, Invalid, Invalid, Valid, Valid)]
    [InlineData(Valid, Invalid, Invalid, Valid, Invalid, Invalid)]
    [InlineData(Valid, Invalid, Invalid, Valid, Invalid, Valid)]
    [InlineData(Valid, Invalid, Invalid, Valid, Valid, Invalid)]
    [InlineData(Valid, Invalid, Invalid, Valid, Valid, Valid)]
    [InlineData(Valid, Invalid, Valid, Invalid, Invalid, Invalid)]
    [InlineData(Valid, Invalid, Valid, Invalid, Invalid, Valid)]
    [InlineData(Valid, Invalid, Valid, Invalid, Valid, Invalid)]
    [InlineData(Valid, Invalid, Valid, Invalid, Valid, Valid)]
    [InlineData(Valid, Invalid, Valid, Valid, Invalid, Invalid)]
    [InlineData(Valid, Invalid, Valid, Valid, Invalid, Valid)]
    [InlineData(Valid, Invalid, Valid, Valid, Valid, Invalid)]
    [InlineData(Valid, Valid, Invalid, Invalid, Invalid, Invalid)]
    [InlineData(Valid, Valid, Invalid, Invalid, Invalid, Valid)]
    [InlineData(Valid, Valid, Invalid, Invalid, Valid, Invalid)]
    [InlineData(Valid, Valid, Invalid, Invalid, Valid, Valid)]
    [InlineData(Valid, Valid, Invalid, Valid, Invalid, Invalid)]
    [InlineData(Valid, Valid, Invalid, Valid, Invalid, Valid)]
    [InlineData(Valid, Valid, Invalid, Valid, Valid, Invalid)]
    [InlineData(Valid, Valid, Valid, Invalid, Invalid, Invalid)]
    [InlineData(Valid, Valid, Valid, Invalid, Invalid, Valid)]
    [InlineData(Valid, Valid, Valid, Invalid, Valid, Invalid)]
    [InlineData(Valid, Valid, Valid, Valid, Invalid, Invalid)]
    public void ShouldNotBeSatisfiedBySeveralInvalidFields(
        byte street,
        byte number,
        byte neighborhood,
        byte city,
        byte state,
        byte zipCode
    )
    {
        var addressSpecificationDto = new AddressSpecificationDto(
            street: street == Valid ? Street : string.Empty,
            number: number == Valid ? Number : string.Empty,
            neighborhood: neighborhood == Valid ? Neighborhood : string.Empty,
            city: city == Valid ? City : string.Empty,
            state: state == Valid ? State : string.Empty,
            zipCode: zipCode == Valid ? ZipCode : string.Empty
        );

        var isSatisfiedBy = _addressSpecification.IsSatisfiedBy(
            data: addressSpecificationDto
        );

        Assert.False(isSatisfiedBy);

        Assert.Equal(
            expected: AppStatusCode.SeveralInvalidFields,
            actual: _addressSpecification.AppStatusCode
        );

        Assert.NotEmpty(_addressSpecification.ErrorMessages);

        if (street == Invalid)
        {
            Assert.Contains(
                expected: AddressMessages.StreetIsInvalid,
                collection: _addressSpecification.ErrorMessages
            );
        }

        if (number == Invalid)
        {
            Assert.Contains(
                expected: AddressMessages.AddressNumberIsInvalid,
                collection: _addressSpecification.ErrorMessages
            );
        }

        if (neighborhood == Invalid)
        {
            Assert.Contains(
                expected: AddressMessages.NeighborhoodIsInvalid,
                collection: _addressSpecification.ErrorMessages
            );
        }

        if (city == Invalid)
        {
            Assert.Contains(
                expected: AddressMessages.CityIsInvalid,
                collection: _addressSpecification.ErrorMessages
            );
        }

        if (zipCode == Invalid)
        {
            Assert.Contains(
                expected: AddressMessages.ZipCodeIsInvalid,
                collection: _addressSpecification.ErrorMessages
            );
        }

        if (state == Invalid)
        {
            Assert.Contains(
                expected: AddressMessages.StateIsInvalid,
                collection: _addressSpecification.ErrorMessages
            );
        }
    }

    # endregion
}
