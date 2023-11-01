using System.Diagnostics.CodeAnalysis;
using System.Net;
using BrazilianTypes.Types;
using Lira.Application.CQRS.Managers.Commands.CreateAdmin;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Domain.Domains.Manager;
using Lira.Domain.Domains.Person;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Lira.Test.Commands.Managers;

[ExcludeFromCodeCoverage]
public class CreateAdminTest
{
    # region ---- mocks --------------------------------------------------------

    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IManagerRepository> _managerRepositoryMock;
    private readonly CreateAdminHandler _handler;

    # endregion

    # region ---- data ---------------------------------------------------------

    private readonly CreateAdminRequest _request;

    private readonly Guid _personId = Guid.NewGuid();
    private readonly Guid _managerId = Guid.NewGuid();

    private const string Code = "validCode";
    private const string InvalidCode = "invalidCode";

    private const string Password = "A1234567";

    private readonly Cpf _cpf = Cpf.Generate();

    private const string Name = "john";
    private const string Surname = "doe";
    private const string Username = "jdoe";

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateAdminTest()
    {
        _configurationMock = new Mock<IConfiguration>();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _managerRepositoryMock = new Mock<IManagerRepository>();

        SetupMocks();

        _request = new CreateAdminRequest(
            name: Name,
            surname: Surname,
            cpf: _cpf,
            code: Code,
            password: Password,
            passwordConfirmation: Password,
            username: Username
        );

         _handler = new CreateAdminHandler(
            _configurationMock.Object,
            _personRepositoryMock.Object,
            _managerRepositoryMock.Object
        );
    }

    # endregion

    # region ---- mock setup ---------------------------------------------------

    private void SetupMocks()
    {
        _configurationMock
            .Setup(config => config["Admin:Code"])
            .Returns(Code);

        _personRepositoryMock
            .Setup(repo => repo.CreateAsync(
                It.IsAny<PersonDomain>()
            ))
            .ReturnsAsync(new PersonDomain(
                id: _personId,
                name: Name,
                surname: Surname,
                cpf: _cpf,
                createdAt: DateTime.UtcNow
            ));

        _personRepositoryMock
            .Setup(repo => repo.FindByCpfAsync(
                It.IsAny<Cpf>(),
                false,
                false,
                false,
                false,
                false,
                false,
                false
            ))
            .ReturnsAsync(null as PersonDomain);

        _managerRepositoryMock
            .Setup(repo => repo.CreateAsync(
                It.IsAny<ManagerDomain>()
            ))
            .ReturnsAsync(new ManagerDomain(
                id: _managerId,
                username: Username,
                password: Password,
                personId: _personId,
                createdAt: DateTime.UtcNow
            ));

        _managerRepositoryMock
            .Setup(repo => repo.FindAllAsync(
                false,
                false
            ))
            .ReturnsAsync(new List<ManagerDomain>());
    }

    # endregion

    # region ---- should create ------------------------------------------------

    [Fact]
    public async void ShouldCreateAdmin()
    {
        # region ---- act ------------------------------------------------------

        var response = await _handler.Handle(
            request: _request,
            CancellationToken.None
        );

        # endregion

        # region ---- assert ---------------------------------------------------

        Assert.True(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.Created,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: StatusCode.CreatedTransaction,
            actual: response.StatusCode
        );

        Assert.Null(response.Error);
        Assert.Null(response.Pagination);

        Assert.NotNull(response.Data);

        Assert.Equal(
            expected: _managerId,
            actual: response.Data.Id
        );

        # endregion
    }

    # endregion

    # region ---- should not create more than one ------------------------------

    [Fact]
    public async void ShouldNotCreateMoreThanOneAdmin()
    {
        # region ---- arrange --------------------------------------------------

        _managerRepositoryMock
            .Setup(repo => repo.FindAllAsync(
                false,
                false
            ))
            .ReturnsAsync(new List<ManagerDomain>
            {
                ManagerDomain.Create(
                    username: Username,
                    password: Password,
                    personId: _personId
                )
            });

        # endregion

        # region ---- act ------------------------------------------------------

        var response = await _handler.Handle(
            request: _request,
            CancellationToken.None
        );

        # endregion

        # region ---- assert ---------------------------------------------------

        Assert.False(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.UnprocessableEntity,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: StatusCode.AdminAlreadyExists,
            actual: response.StatusCode
        );

        Assert.NotNull(response.Error);

        Assert.Equal(
            expected: AccountsMessages.AdminAlreadyExists,
            actual: response.Error?.Messages.First()
        );

        Assert.Null(response.Pagination);
        Assert.Null(response.Data);

        # endregion
    }

    # endregion

    # region ---- should not create if code is not valid -----------------------

    [Fact]
    public async void ShouldNotCreateIfCodeIsInvalid()
    {
        # region ---- arrange --------------------------------------------------

        var request = new CreateAdminRequest(
            name: Name,
            surname: Surname,
            cpf: _cpf,
            code: InvalidCode,
            password: Password,
            passwordConfirmation: Password,
            username: Username
        );

        # endregion

        # region ---- act ------------------------------------------------------

        var response = await _handler.Handle(
            request: request,
            CancellationToken.None
        );

        # endregion

        # region ---- assert ---------------------------------------------------

        Assert.False(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.BadRequest,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: StatusCode.AdminCodeIsInvalid,
            actual: response.StatusCode
        );

        Assert.NotNull(response.Error);

        Assert.Equal(
            expected: AccountsMessages.AdminCodeIsInvalid,
            actual: response.Error?.Messages.First()
        );

        Assert.Null(response.Pagination);
        Assert.Null(response.Data);

        # endregion
    }

    # endregion
}
