using System.Diagnostics.CodeAnalysis;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Application.Specifications.Address;

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
            expected: StatusCode.Empty,
            actual: _addressSpecification.StatusCode
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
            expected: StatusCode.StreetIsInvalid,
            actual: _addressSpecification.StatusCode
        );
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
            expected: StatusCode.AddressNumberIsInvalid,
            actual: _addressSpecification.StatusCode
        );
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
            expected: StatusCode.NeighborhoodIsInvalid,
            actual: _addressSpecification.StatusCode
        );
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
            expected: StatusCode.CityIsInvalid,
            actual: _addressSpecification.StatusCode
        );
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
            expected: StatusCode.ZipCodeIsInvalid,
            actual: _addressSpecification.StatusCode
        );

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
            expected: StatusCode.UfIsInvalid,
            actual: _addressSpecification.StatusCode
        );

        Assert.Contains(
            expected: AddressMessages.StateIsInvalid,
            collection: _addressSpecification.ErrorMessages
        );
    }

    # endregion

    # region ---- SeveralInvalidFields -----------------------------------------

    [Theory]
    [InlineData("", "", "", "", "", "")]
    [InlineData("", "", "", "", "", ZipCode)]
    [InlineData("", "", "", "", State, "")]
    [InlineData("", "", "", "", State, ZipCode)]
    [InlineData("", "", "", City, "", "")]
    [InlineData("", "", "", City, "", ZipCode)]
    [InlineData("", "", "", City, State, "")]
    [InlineData("", "", "", City, State, ZipCode)]
    [InlineData("", "", Neighborhood, "", "", "")]
    [InlineData("", "", Neighborhood, "", "", ZipCode)]
    [InlineData("", "", Neighborhood, "", State, "")]
    [InlineData("", "", Neighborhood, "", State, ZipCode)]
    [InlineData("", "", Neighborhood, City, "", "")]
    [InlineData("", "", Neighborhood, City, "", ZipCode)]
    [InlineData("", "", Neighborhood, City, State, "")]
    [InlineData("", "", Neighborhood, City, State, ZipCode)]
    [InlineData("", Number, "", "", "", "")]
    [InlineData("", Number, "", "", "", ZipCode)]
    [InlineData("", Number, "", "", State, "")]
    [InlineData("", Number, "", "", State, ZipCode)]
    [InlineData("", Number, "", City, "", "")]
    [InlineData("", Number, "", City, "", ZipCode)]
    [InlineData("", Number, "", City, State, "")]
    [InlineData("", Number, "", City, State, ZipCode)]
    [InlineData("", Number, Neighborhood, "", "", "")]
    [InlineData("", Number, Neighborhood, "", "", ZipCode)]
    [InlineData("", Number, Neighborhood, "", State, "")]
    [InlineData("", Number, Neighborhood, "", State, ZipCode)]
    [InlineData("", Number, Neighborhood, City, "", "")]
    [InlineData("", Number, Neighborhood, City, "", ZipCode)]
    [InlineData("", Number, Neighborhood, City, State, "")]
    [InlineData(Street, " ", " ", " ", " ", " ")]
    [InlineData(Street, " ", " ", " ", " ", ZipCode)]
    [InlineData(Street, " ", " ", " ", State, " ")]
    [InlineData(Street, " ", " ", " ", State, ZipCode)]
    [InlineData(Street, " ", " ", City, " ", " ")]
    [InlineData(Street, " ", " ", City, " ", ZipCode)]
    [InlineData(Street, " ", " ", City, State, " ")]
    [InlineData(Street, " ", " ", City, State, ZipCode)]
    [InlineData(Street, " ", Neighborhood, " ", " ", " ")]
    [InlineData(Street, " ", Neighborhood, " ", " ", ZipCode)]
    [InlineData(Street, " ", Neighborhood, " ", State, " ")]
    [InlineData(Street, " ", Neighborhood, " ", State, ZipCode)]
    [InlineData(Street, " ", Neighborhood, City, " ", " ")]
    [InlineData(Street, " ", Neighborhood, City, " ", ZipCode)]
    [InlineData(Street, " ", Neighborhood, City, State, " ")]
    [InlineData(Street, Number, " ", " ", " ", " ")]
    [InlineData(Street, Number, " ", " ", " ", ZipCode)]
    [InlineData(Street, Number, " ", " ", State, " ")]
    [InlineData(Street, Number, " ", " ", State, ZipCode)]
    [InlineData(Street, Number, " ", City, " ", " ")]
    [InlineData(Street, Number, " ", City, " ", ZipCode)]
    [InlineData(Street, Number, " ", City, State, " ")]
    [InlineData(Street, Number, Neighborhood, " ", " ", " ")]
    [InlineData(Street, Number, Neighborhood, " ", " ", ZipCode)]
    [InlineData(Street, Number, Neighborhood, " ", State, " ")]
    [InlineData(Street, Number, Neighborhood, City, " ", " ")]
    public void ShouldNotBeSatisfiedBySeveralInvalidFields(
        string street,
        string number,
        string neighborhood,
        string city,
        string state,
        string zipCode
    )
    {
        var addressSpecificationDto = new AddressSpecificationDto(
            street: street,
            number: number,
            neighborhood: neighborhood,
            city: city,
            state: state,
            zipCode: zipCode
        );

        var isSatisfiedBy = _addressSpecification.IsSatisfiedBy(
            data: addressSpecificationDto
        );

        Assert.False(isSatisfiedBy);


        Assert.Equal(
            expected: StatusCode.SeveralInvalidFields,
            actual: _addressSpecification.StatusCode
        );

        if (street != Street)
        {
            Assert.Contains(
                expected: AddressMessages.StreetIsInvalid,
                collection: _addressSpecification.ErrorMessages
            );
        }

        if (number != Number)
        {
            Assert.Contains(
                expected: AddressMessages.AddressNumberIsInvalid,
                collection: _addressSpecification.ErrorMessages
            );
        }

        if (neighborhood != Neighborhood)
        {
            Assert.Contains(
                expected: AddressMessages.NeighborhoodIsInvalid,
                collection: _addressSpecification.ErrorMessages
            );
        }

        if (city != City)
        {
            Assert.Contains(
                expected: AddressMessages.CityIsInvalid,
                collection: _addressSpecification.ErrorMessages
            );
        }

        if (zipCode != ZipCode)
        {
            Assert.Contains(
                expected: AddressMessages.ZipCodeIsInvalid,
                collection: _addressSpecification.ErrorMessages
            );
        }

        if (state != State)
        {
            Assert.Contains(
                expected: AddressMessages.StateIsInvalid,
                collection: _addressSpecification.ErrorMessages
            );
        }
    }

    # endregion
}
