using System.Diagnostics.CodeAnalysis;
using System.Net;
using BrazilianTypes.Types;
using Lira.Application.CQRS.Managers.Commands.CreateManager;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Common.Types;
using Lira.Domain.Domains.Manager;
using Lira.Domain.Domains.Person;
using Moq;

namespace Lira.Test.Commands.Managers;

[ExcludeFromCodeCoverage]
public class CreateManagerTest
{
    # region ---- properties ---------------------------------------------------

    private readonly Mock<IManagerRepository> _managerRepositoryMock;
    private readonly Mock<IPersonRepository> _personRepositoryMock;

    private readonly CreateManagerRequest _request;
    private readonly CreateManagerHandler _handler;

    private const string Password = "A1234567";
    private const string Username = "jdoe";
    private static readonly Guid PersonId = Guid.NewGuid();
    private static readonly Guid ManagerId = Guid.NewGuid();

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateManagerTest()
    {
        _managerRepositoryMock = new Mock<IManagerRepository>();
        _personRepositoryMock = new Mock<IPersonRepository>();

        SetupMocks();

        _request = new CreateManagerRequest(
            username: Username,
            password: Password,
            confirmation: Password,
            personId: PersonId
        );

        _handler = new CreateManagerHandler(
            personRepository: _personRepositoryMock.Object,
            managerRepository: _managerRepositoryMock.Object
        );
    }

    # endregion

    # region ---- setup --------------------------------------------------------

    private void SetupMocks()
    {
        _managerRepositoryMock
            .Setup(repo => repo.CreateAsync(
                It.IsAny<ManagerDomain>()
            ))
            .ReturnsAsync(new ManagerDomain(
                id: ManagerId,
                username: Username,
                password: Password,
                personId: PersonId,
                createdAt: DateTime.UtcNow
            ));

        _personRepositoryMock
            .Setup(repo => repo.FindByIdAsync(
                It.IsAny<Guid>(),
                false,
                false,
                false,
                false,
                false,
                false,
                false
            ))
            .ReturnsAsync(new PersonDomain(
                id: PersonId,
                name: "John",
                surname: "Doe",
                cpf: Cpf.Generate(),
                createdAt: DateTime.UtcNow
            ));
    }

    # endregion

    # region ---- success ------------------------------------------------------

    [Fact]
    public async void ShouldCreateManager()
    {
        var response = await _handler.Handle(
            request: _request,
            cancellationToken: CancellationToken.None
        );

        Assert.True(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.Created,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: StatusCode.CreatedOne,
            actual: response.StatusCode
        );

        Assert.Null(response.Error);
        Assert.Null(response.Pagination);

        Assert.NotNull(response.Data);

        Assert.Equal(
            expected: ManagerId,
            actual: response.Data.Id
        );
    }

    # endregion

    # region ---- person not found ---------------------------------------------

    [Fact]
    public async void ShouldReturnPersonNotFound()
    {
        _personRepositoryMock
            .Setup(repo => repo.FindByIdAsync(
                It.IsAny<Guid>(),
                false,
                false,
                false,
                false,
                false,
                false,
                false
            ))
            .ReturnsAsync(null as PersonDomain);

        var response = await _handler.Handle(
            request: _request,
            cancellationToken: CancellationToken.None
        );

        Assert.False(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.NotFound,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: StatusCode.PersonNotFound,
            actual: response.StatusCode
        );

        Assert.NotNull(response.Error);

        Assert.Equal(
            expected: NotFoundMessages.PersonNotFound,
            actual: response.Error?.Messages.FirstOrDefault()
        );

        Assert.Null(response.Pagination);
        Assert.Null(response.Data);
    }

    # endregion

    # region ---- username already exists --------------------------------------

    [Fact]
    public async void ShouldReturnUsernameAlreadyExists()
    {
        _managerRepositoryMock
            .Setup(repo => repo.FindByUsernameAsync(
                It.IsAny<Username>(),
                false,
                false
            ))
            .ReturnsAsync(new ManagerDomain(
                id: Guid.NewGuid(),
                username: Username,
                password: Password,
                personId: PersonId,
                createdAt: DateTime.UtcNow
            ));

        var response = await _handler.Handle(
            request: _request,
            cancellationToken: CancellationToken.None
        );

        Assert.False(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.BadRequest,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: StatusCode.UsernameAlreadyExists,
            actual: response.StatusCode
        );

        Assert.NotNull(response.Error);

        Assert.Equal(
            expected: ConflictMessages.UsernameIsInUse,
            actual: response.Error?.Messages.FirstOrDefault()
        );

        Assert.Null(response.Pagination);
        Assert.Null(response.Data);
    }

    # endregion

    # region ---- password specification ---------------------------------------

    [Fact]
    public async void ShouldFailPasswordSpecification()
    {
        var request = new CreateManagerRequest(
            username: Username,
            password: "12345678",
            confirmation: "1234567",
            personId: PersonId
        );

        var response = await _handler.Handle(
            request: request,
            cancellationToken: CancellationToken.None
        );

        Assert.False(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.BadRequest,
            actual: response.HttpStatusCode
        );

        Assert.NotNull(response.Error);
        Assert.NotNull(response.Error?.Messages);
        Assert.Null(response.Pagination);
        Assert.Null(response.Data);
    }

    # endregion

    # region ---- manager specification ---------------------------------------

    [Fact]
    public async void ShouldFailManagerSpecification()
    {
        var request = new CreateManagerRequest(
            username: "1jdoe",
            password: Password,
            confirmation: Password,
            personId: PersonId
        );

        var response = await _handler.Handle(
            request: request,
            cancellationToken: CancellationToken.None
        );

        Assert.False(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.BadRequest,
            actual: response.HttpStatusCode
        );

        Assert.NotNull(response.Error);
        Assert.NotNull(response.Error?.Messages);
        Assert.Null(response.Pagination);
        Assert.Null(response.Data);
    }

    # endregion
}
