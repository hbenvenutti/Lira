using System.Diagnostics.CodeAnalysis;
using Lira.Common.Types;

namespace Lira.Test.Types;

[ExcludeFromCodeCoverage]
public class PasswordTest
{
    [Theory]
    [InlineData("A1234567")]
    [InlineData("a1234567")]
    [InlineData("1234567a")]
    [InlineData("password")]
    [InlineData("strong-password")]
    [InlineData("@a2345678")]
    [InlineData(" A1234567 ")]
    [InlineData("A1234567 ")]
    [InlineData(" A1234567")]

    public void ItShouldCreateAPassword(string password)
    {
        Password parsedPassword = password;

        Assert.Equal(
            expected: password.Trim(),
            actual: parsedPassword.ToString()
        );

        Assert.NotEqual(
            expected: password,
            actual: parsedPassword.Hash
        );
    }

    [Theory]
    [InlineData("A123456")]
    [InlineData("12345678")]
    [InlineData("123456789")]
    [InlineData("1234")]
    [InlineData("mini")]
    [InlineData("")]
    public void ItShouldThrowAnException(string password)
    {
        Assert.Throws<ArgumentException>(
            testCode: () =>
            {
                Password _ = password;
            }
        );
    }
}
