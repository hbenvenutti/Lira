using System.Diagnostics.CodeAnalysis;
using Lira.Common.Enums;
using Lira.Common.Types;
using Lira.Domain.Domains.Manager;

namespace Lira.Test.Specifications;

[ExcludeFromCodeCoverage]
public class ManagerSpecificationTest
{
    private readonly ManagerSpecification _managerSpecification = new();
    private static readonly Username Username = "JohnDoe";

    # region ---- IsSatisfiedBy ------------------------------------------------

    [Fact]
    public void IsSatisfiedBy_WhenUsernameIsInvalid_ReturnsFalse()
    {
        var managerSpecificationDto = new ManagerSpecificationDto(Username);

        var result = _managerSpecification
            .IsSatisfiedBy(managerSpecificationDto);

        Assert.True(result);
        Assert.Equal(
            expected: AppStatusCode.Empty,
            actual: _managerSpecification.AppStatusCode
        );

        Assert.NotNull(_managerSpecification.ErrorMessages);
        Assert.Empty(collection:_managerSpecification.ErrorMessages);
    }

    # endregion

    # region ---- invalid username ---------------------------------------------

    [Fact]
    public void ShouldFailIfUsernameIsInvalid()
    {
        var managerSpecificationDto = new ManagerSpecificationDto(
            username: string.Empty
        );

        var result = _managerSpecification
            .IsSatisfiedBy(managerSpecificationDto);

        Assert.False(result);
        Assert.Equal(
            expected: AppStatusCode.InvalidUsername,
            actual: _managerSpecification.AppStatusCode
        );

        Assert.NotNull(_managerSpecification.ErrorMessages);
        Assert.NotEmpty(collection:_managerSpecification.ErrorMessages);
        Assert.Contains(
            expected: Username.ErrorMessage,
            collection : _managerSpecification.ErrorMessages
        );
    }

    # endregion
}
