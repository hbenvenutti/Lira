using System.Diagnostics.CodeAnalysis;
using BrazilianTypes.Types;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Application.Specifications.Phones;

namespace Lira.Test.Specifications;

[ExcludeFromCodeCoverage]
public class PhoneSpecificationTest
{
    private readonly PhoneSpecification _phoneSpecification = new();

    private static readonly Phone Phone = "11999999999";
    private const string InvalidPhone = "99999999";

    # region ---- success ------------------------------------------------------

    [Fact]
    public void ShouldBeValid()
    {
        var data = new PhoneSpecificationDto(
            phone: Phone
        );

        var result = _phoneSpecification.IsSatisfiedBy(data);

        Assert.True(result);

        Assert.Equal(
            expected: StatusCode.Empty,
            actual: _phoneSpecification.StatusCode
        );

        Assert.NotNull(_phoneSpecification.ErrorMessages);
        Assert.Empty(_phoneSpecification.ErrorMessages);
    }

    # endregion

    # region ---- phone is invalid ---------------------------------------------

    [Fact]
    public void ShouldNotBeValidWhenPhoneIsInvalid()
    {
        var data = new PhoneSpecificationDto(
            phone: InvalidPhone
        );

        var result = _phoneSpecification.IsSatisfiedBy(data);

        Assert.False(result);

        Assert.Equal(
            expected: StatusCode.PhoneIsInvalid,
            actual: _phoneSpecification.StatusCode
        );

        Assert.NotNull(_phoneSpecification.ErrorMessages);
        Assert.NotEmpty(_phoneSpecification.ErrorMessages);
        Assert.Contains(
            expected: PersonMessages.InvalidPhone,
            collection: _phoneSpecification.ErrorMessages
        );
    }

    # endregion
}
