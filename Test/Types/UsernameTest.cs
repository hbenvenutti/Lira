using System.Diagnostics.CodeAnalysis;
using Lira.Common.Types;

namespace Lira.Test.Types;

[ExcludeFromCodeCoverage]
public class UsernameTest
{
    [Theory]
    [InlineData("A12")]
    [InlineData("a12")]
    [InlineData(" A12 ")]
    [InlineData("A_1")]
    [InlineData("a_1")]
    [InlineData("a12345678901234")]
    [InlineData(" a1234567890123 ")]
    [InlineData("a1234_567890123")]

    public void ItShouldCreateAUsername(string username)
    {
        Username parsedUsername = username;

        Assert.Equal(
            expected: username.Trim(),
            actual: parsedUsername
        );
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    [InlineData("_")]
    [InlineData("_a")]
    [InlineData("_1")]
    [InlineData("1")]
    [InlineData("11")]
    [InlineData("111")]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("A123456789012345")]
    [InlineData("UsernameIsToLong")]
    [InlineData("a.2")]
    [InlineData("a-2")]
    [InlineData("a 2")]
    [InlineData("a@2")]
    [InlineData("a_")]
    [InlineData("a#2")]
    public void ItShouldThrowAnException(string username)
    {
        Assert.Throws<ArgumentException>(
            testCode: () => { Username _ = username; }
        );
    }
}
