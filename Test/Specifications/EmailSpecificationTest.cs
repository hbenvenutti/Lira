using System.Diagnostics.CodeAnalysis;
using BrazilianTypes.Types;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Application.Specifications.Emails;

namespace Lira.Test.Specifications;

[ExcludeFromCodeCoverage]
public class EmailSpecificationTest
{
    private readonly EmailSpecification _emailSpecification = new();

    private static readonly Email Email = "foobar@gmail.com";
    private const string InvalidEmail = "";

    # region ---- success ------------------------------------------------------

    [Fact]
    public void ShouldBeSatisfiedBy()
    {
        var data = new EmailSpecificationDto(Email);

        var result = _emailSpecification.IsSatisfiedBy(data);

        Assert.True(result);
        Assert.Equal(
            expected: StatusCode.Empty,
            actual: _emailSpecification.StatusCode
        );

        Assert.NotNull(_emailSpecification.ErrorMessages);
        Assert.Empty(_emailSpecification.ErrorMessages);
    }

    # endregion

    # region ---- failure ------------------------------------------------------

    [Fact]
    public void ShouldNotBeSatisfiedBy()
    {
        var data = new EmailSpecificationDto(address: InvalidEmail);

        var result = _emailSpecification.IsSatisfiedBy(data);

        Assert.False(result);

        Assert.Equal(
            expected: StatusCode.InvalidEmailAddress,
            actual: _emailSpecification.StatusCode
        );

        Assert.NotNull(_emailSpecification.ErrorMessages);
        Assert.NotEmpty(_emailSpecification.ErrorMessages);
        Assert.Contains(
            expected: PersonMessages.InvalidEmail,
            collection: _emailSpecification.ErrorMessages
        );
    }

    # endregion
}
