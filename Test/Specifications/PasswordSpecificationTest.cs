using System.Diagnostics.CodeAnalysis;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Application.Specifications.Passwords;
using Lira.Common.Types;

namespace Lira.Test.Specifications;

[ExcludeFromCodeCoverage]
public class PasswordSpecificationTest
{
    private readonly PasswordSpecification _passwordSpecification = new();

    private static readonly Password Password = "Password123";
    private const string InvalidPassword = "";

    # region ---- success ------------------------------------------------------

    [Fact]
    public void ShouldBeValid()
    {
        var data = new PasswordSpecificationDto(
            password: Password,
            confirmation:Password
        );

        var result = _passwordSpecification.IsSatisfiedBy(data);

        Assert.True(result);

        Assert.Equal(
            expected: StatusCode.Empty,
            actual: _passwordSpecification.StatusCode
        );

        Assert.NotNull(_passwordSpecification.ErrorMessages);
        Assert.Empty(_passwordSpecification.ErrorMessages);
    }

    # endregion

    # region ---- passwords do not match ---------------------------------------

    [Fact]
    public void ShouldNotBeValidWhenPasswordsDoNotMatch()
    {
        var data = new PasswordSpecificationDto(
            password: Password,
            confirmation: InvalidPassword
        );

        var result = _passwordSpecification.IsSatisfiedBy(data);

        Assert.False(result);

        Assert.Equal(
            expected: StatusCode.PasswordsDoNotMatch,
            actual: _passwordSpecification.StatusCode
        );

        Assert.NotNull(_passwordSpecification.ErrorMessages);
        Assert.NotEmpty(_passwordSpecification.ErrorMessages);

        Assert.Contains(
            expected: ManagerMessages.PasswordsDoNotMatch,
            collection: _passwordSpecification.ErrorMessages
        );
    }

    # endregion

    # region ---- password is invalid ------------------------------------------

    [Fact]
    public void ShouldNotBeValidWhenPasswordIsInvalid()
    {
        var data = new PasswordSpecificationDto(
            password: InvalidPassword,
            confirmation: InvalidPassword
        );

        var result = _passwordSpecification.IsSatisfiedBy(data);

        Assert.False(result);

        Assert.Equal(
            expected: StatusCode.InvalidPassword,
            actual: _passwordSpecification.StatusCode
        );

        Assert.NotNull(_passwordSpecification.ErrorMessages);
        Assert.NotEmpty(_passwordSpecification.ErrorMessages);

        Assert.Contains(
            expected: ManagerMessages.InvalidPassword,
            collection: _passwordSpecification.ErrorMessages
        );
    }

    # endregion
}
