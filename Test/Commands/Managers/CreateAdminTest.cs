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

    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly Mock<IManagerRepository> _managerRepositoryMock;
    private readonly CreateAdminHandler _handler;

    # endregion

    # region ---- data ---------------------------------------------------------

    private readonly Guid _personId = Guid.NewGuid();
    private readonly Guid _managerId = Guid.NewGuid();

    private const string Code = "validCode";
    private const string InvalidCode = "invalidCode";

    private const string Password = "A1234567";

    private const string Cpf = "854.247.400-73";
    private const string Cpf2 = "854247400-73";
    private const string Cpf3 = "85424740073";

    private const string Name = "john";
    private const string Surname = "doe";
    private const string Username = "jdoe";

    private const string NameComposite = "john foo";
    private const string SurnameComposite = "doe bar";

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateAdminTest()
    {
         var configurationMock = new Mock<IConfiguration>();
        _personRepositoryMock = new Mock<IPersonRepository>();
        _managerRepositoryMock = new Mock<IManagerRepository>();

        configurationMock
            .Setup(config => config["Admin:Code"])
            .Returns(Code);

         _handler = new CreateAdminHandler(
            configurationMock.Object,
            _personRepositoryMock.Object,
            _managerRepositoryMock.Object
        );
    }

    # endregion

    # region ---- should create ------------------------------------------------

    [Theory]
    [InlineData(
        Name,
        Surname,
        Cpf,
        Code,
        Password,
        Username
    )]
    [InlineData(
        $" {Name} ",
        $" {Surname} ",
        Cpf2,
        Code,
        Password,
        $" {Username} "
    )]
    [InlineData(
        NameComposite,
        SurnameComposite,
        Cpf3,
        Code,
        Password,
        Username
    )]
    public async void ShouldCreateAdmin(
        string name,
        string surname,
        string cpf,
        string code,
        string password,
        string username
    )
    {
        # region ---- arrange --------------------------------------------------

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

        _personRepositoryMock
            .Setup(repo => repo.CreateAsync(
                It.IsAny<PersonDomain>()
            ))
            .ReturnsAsync(new PersonDomain(
                id: _personId,
                name: name,
                surname: surname,
                cpf: cpf,
                createdAt: DateTime.UtcNow
            ));

        _managerRepositoryMock
            .Setup(repo => repo.FindAllAsync(
                false,
                false
            ))
            .ReturnsAsync(new List<ManagerDomain>());

        _managerRepositoryMock
            .Setup(repo => repo.CreateAsync(
                It.IsAny<ManagerDomain>()
            ))
            .ReturnsAsync(new ManagerDomain(
                id: _managerId,
                username: username,
                password: password,
                personId: _personId,
                createdAt: DateTime.UtcNow
            ));

        var request = new CreateAdminRequest(
            name: name,
            surname: surname,
            cpf: cpf,
            code: code,
            password: password,
            passwordConfirmation: password,
            username: username
        );

        # endregion

        # region ---- act ------------------------------------------------------

        var response = await _handler.Handle(
            request: request,
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

    [Theory]
    [InlineData(
        Name,
        Surname,
        Cpf,
        Code,
        Password,
        Username
    )]
    [InlineData(
        $" {Name} ",
        $" {Surname} ",
        Cpf2,
        Code,
        Password,
        $" {Username} "
    )]
    [InlineData(
        NameComposite,
        SurnameComposite,
        Cpf3,
        Code,
        Password,
        Username
    )]
    public async void ShouldNotCreateMoreThanOneAdmin(
        string name,
        string surname,
        string cpf,
        string code,
        string password,
        string username
    )
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
                    username: username,
                    password: password,
                    personId: Guid.NewGuid()
                )
            });

        var request = new CreateAdminRequest(
            name: name,
            surname: surname,
            cpf: cpf,
            code: code,
            password: password,
            passwordConfirmation: password,
            username: username
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

    [Theory]
    [InlineData(
        Name,
        Surname,
        Cpf,
        InvalidCode,
        Password,
        Username
    )]
    [InlineData(
        $" {Name} ",
        $" {Surname} ",
        Cpf2,
        InvalidCode,
        Password,
        $" {Username} "
    )]
    [InlineData(
        NameComposite,
        SurnameComposite,
        Cpf3,
        InvalidCode,
        Password,
        Username
    )]
    public async void ShouldNotCreateIfCodeIsInvalid(
        string name,
        string surname,
        string cpf,
        string code,
        string password,
        string username
    )
    {
        # region ---- arrange --------------------------------------------------

        var request = new CreateAdminRequest(
            name: name,
            surname: surname,
            cpf: cpf,
            code: code,
            password: password,
            passwordConfirmation: password,
            username: username
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
