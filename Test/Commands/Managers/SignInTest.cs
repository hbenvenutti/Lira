using System.Diagnostics.CodeAnalysis;
using System.Net;
using Lira.Application.CQRS.Accounts.Commands.Login;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Application.Services.Token;
using Lira.Common.Providers.Hash;
using Lira.Common.Types;
using Lira.Domain.Authentication.Manager;
using Lira.Domain.Domains.Manager;
using Moq;

namespace Lira.Test.Commands.Managers;

[ExcludeFromCodeCoverage]
public class SignInTest
{
    private readonly Mock<IManagerRepository> _managerRepositoryMock;
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly IHashService _hashService = new HashService();

    private readonly SignInHandler _handler;

    private const string Password = "A1234567";
    private const string WrongPassword = "A12345678";
    private const string InvalidPassword = "12345678";
    private const string Username = "jdoe";
    private const string WrongUsername = "jdoe2";
    private const string InvalidUsername = "2jdoe";
    private const string Token = "token";


    # region ---- constructor --------------------------------------------------

    public SignInTest()
    {
        _managerRepositoryMock = new Mock<IManagerRepository>();
        _tokenServiceMock = new Mock<ITokenService>();

        SetupMocks();

        _handler = new SignInHandler(
            _managerRepositoryMock.Object,
            _tokenServiceMock.Object
        );
    }

    # endregion

    # region ---- setup mocks --------------------------------------------------

    private void SetupMocks()
    {
        _tokenServiceMock
            .Setup(tokenService => tokenService
                .Sign(It.IsAny<ManagerAuthDomain>())
            )
            .Returns(Token);

        _managerRepositoryMock
            .Setup(managerRepository => managerRepository
                .FindByUsernameAsync(
                    It.IsAny<Username>(),
                    false,
                    false
                )
            )
            .ReturnsAsync(null as ManagerDomain);


        _managerRepositoryMock
            .Setup(managerRepository => managerRepository
                .FindByUsernameAsync(
                    Username,
                    false,
                    false
                )
            )
            .ReturnsAsync(new ManagerDomain(
                id: Guid.NewGuid(),
                username: Username,
                password: _hashService.Hash(Password),
                personId: Guid.NewGuid(),
                createdAt: DateTime.UtcNow
            ));
    }

    # endregion

    # region ---- should authenticate ------------------------------------------

    [Fact]
    public async Task ShouldAuthenticate()
    {
        var request = new SignInRequest(
            password: Password,
            username: Username
        );

        var response = await _handler.Handle(
            request,
            CancellationToken.None
        );

        Assert.Equal(
            expected: HttpStatusCode.OK,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: StatusCode.SignInSuccess,
            actual: response.StatusCode
        );

        Assert.True(response.IsSuccess);
        Assert.Null(response.Error);
        Assert.Null(response.Pagination);

        Assert.NotNull(response.Data);

        Assert.Equal(
            expected: Token,
            actual: response.Data.Token
        );
    }

    # endregion

    # region ---- should not authenticate --------------------------------------

    [Theory]
    [InlineData(Password, WrongUsername)]
    [InlineData(WrongPassword, Username)]
    [InlineData(WrongPassword, WrongUsername)]

    public async Task ShouldNotAuthenticate(
        string password,
        string username
    )
    {
        var request = new SignInRequest(
            password: password,
            username: username
        );

        var response = await _handler.Handle(
            request,
            CancellationToken.None
        );

        Assert.Equal(
            expected: HttpStatusCode.NotFound,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: StatusCode.SignInFailed,
            actual: response.StatusCode
        );

        Assert.False(response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Equal(
            expected: AccountsMessages.InvalidUsernameOrPassword,
            actual: response.Error?.Messages.First()
        );

        Assert.Null(response.Pagination);
        Assert.Null(response.Data);
    }

    # endregion

    # region ---- should validate data -----------------------------------------

    [Theory]
    [InlineData(Password, InvalidUsername)]
    [InlineData(InvalidPassword, Username)]
    [InlineData(InvalidPassword, InvalidUsername)]

    public async Task ShouldNotAuthenticateWithBadData(
        string password,
        string username
    )
    {
        var request = new SignInRequest(
            password: password,
            username: username
        );

        var response = await _handler.Handle(
            request,
            CancellationToken.None
        );

        Assert.Equal(
            expected: HttpStatusCode.BadRequest,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: StatusCode.SignInFailed,
            actual: response.StatusCode
        );

        Assert.False(response.IsSuccess);
        Assert.NotNull(response.Error);
        Assert.Equal(
            expected: AccountsMessages.InvalidUsernameOrPassword,
            actual: response.Error?.Messages.First()
        );

        Assert.Null(response.Pagination);
        Assert.Null(response.Data);
    }

    # endregion
}
