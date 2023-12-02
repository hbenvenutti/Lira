using System.Diagnostics.CodeAnalysis;
using System.Net;
using BrazilianTypes.Types;
using Lira.Application.CQRS.Managers.Commands.CreateAdmin;
using Lira.Application.CQRS.Managers.Commands.CreateManager;
using Lira.Application.CQRS.People.Commands.CreatePerson;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Domain.Domains.Manager;
using MediatR;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Lira.Test.Commands.Managers;

[ExcludeFromCodeCoverage]
public class CreateAdminTest
{
    # region ---- mocks --------------------------------------------------------

    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<IManagerRepository> _managerRepositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CreateAdminHandler _handler;

    # endregion

    # region ---- data ---------------------------------------------------------

    private readonly CreateAdminRequest _request;

    private static readonly Guid PersonId = Guid.NewGuid();
    private static readonly Guid ManagerId = Guid.NewGuid();
    private static readonly Cpf Cpf = Cpf.Generate();

    private const string Code = "validCode";
    private const string InvalidCode = "invalidCode";

    private const string Name = "john";
    private const string Surname = "doe";
    private const string Username = "jdoe";
    private const string Password = "A1234567";

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateAdminTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _configurationMock = new Mock<IConfiguration>();
        _managerRepositoryMock = new Mock<IManagerRepository>();

        SetupMocks();

        _request = new CreateAdminRequest(
            name: Name,
            surname: Surname,
            cpf: Cpf,
            code: Code,
            password: Password,
            passwordConfirmation: Password,
            username: Username
        );

         _handler = new CreateAdminHandler(
            _configurationMock.Object,
            _managerRepositoryMock.Object,
            _mediatorMock.Object
        );
    }

    # endregion

    # region ---- mock setup ---------------------------------------------------

    private void SetupMocks()
    {
        _configurationMock
            .Setup(config => config["Admin:Code"])
            .Returns(Code);

        _managerRepositoryMock
            .Setup(repo => repo.FindAllAsync(
                false,
                false
            ))
            .ReturnsAsync(new List<ManagerDomain>());

        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreatePersonRequest>(),
                CancellationToken.None
            ))
            .ReturnsAsync(new HandlerResponse<CreatePersonResponse>(
                isSuccess: true,
                httpStatusCode: HttpStatusCode.Created,
                appStatusCode: AppStatusCode.CreatedOne,
                data: new CreatePersonResponse(id: PersonId)
            ));

        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreateManagerRequest>(),
                CancellationToken.None
            ))
            .ReturnsAsync(new HandlerResponse<CreateManagerResponse>(
                isSuccess: true,
                httpStatusCode: HttpStatusCode.Created,
                appStatusCode: AppStatusCode.CreatedOne,
                data: new CreateManagerResponse(id: ManagerId)
            ));
    }

    # endregion

    # region ---- should create ------------------------------------------------

    [Fact]
    public async void ShouldCreateAdmin()
    {
        var response = await _handler.Handle(
            request: _request,
            CancellationToken.None
        );

        Assert.True(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.Created,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: AppStatusCode.CreatedTransaction,
            actual: response.AppStatusCode
        );

        Assert.Null(response.Errors);

        Assert.NotNull(response.Data);

        Assert.Equal(
            expected: ManagerId,
            actual: response.Data.Id
        );
    }

    # endregion

    # region ---- should not create more than one ------------------------------

    [Fact]
    public async void ShouldNotCreateMoreThanOneAdmin()
    {
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
                    personId: PersonId
                )
            });

        var response = await _handler.Handle(
            request: _request,
            CancellationToken.None
        );

        Assert.False(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.UnprocessableEntity,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: AppStatusCode.AdminAlreadyExists,
            actual: response.AppStatusCode
        );

        Assert.NotNull(response.Errors);

        Assert.Single(response.Errors);

        Assert.Contains(
            expected: ManagerMessages.AdminAlreadyExists,
            collection: response.Errors
        );

        Assert.Null(response.Data);
    }

    # endregion

    # region ---- should not create if code is not valid -----------------------

    [Fact]
    public async void ShouldNotCreateIfCodeIsInvalid()
    {
        var request = new CreateAdminRequest(
            name: Name,
            surname: Surname,
            cpf: Cpf,
            code: InvalidCode,
            password: Password,
            passwordConfirmation: Password,
            username: Username
        );

        var response = await _handler.Handle(
            request: request,
            CancellationToken.None
        );

        Assert.False(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.BadRequest,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: AppStatusCode.InvalidAdminCode,
            actual: response.AppStatusCode
        );

        Assert.NotNull(response.Errors);

        Assert.Single(response.Errors);

        Assert.Contains(
            expected: ManagerMessages.InvalidAdminCode,
            collection: response.Errors
        );

        Assert.Null(response.Data);
    }

    # endregion

    # region ---- should not create if person service fails --------------------

    [Fact]
    public async void ShouldNotCreateIfPersonFailsAsync()
    {
        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreatePersonRequest>(),
                CancellationToken.None
            ))
            .ReturnsAsync(new HandlerResponse<CreatePersonResponse>(
                isSuccess: false,
                httpStatusCode: HttpStatusCode.BadGateway,
                appStatusCode: AppStatusCode.Empty,
                errors: PersonMessages.InvalidDocument
            ));

        var result = await _handler.Handle(
            request: _request,
            CancellationToken.None
        );

        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.NotNull(result.Errors);
        Assert.NotEmpty(result.Errors);
    }

    # endregion

    # region ---- should not create if manager service fails -------------------

    [Fact]
    public async void ShouldNotCreateIfManagerFailsAsync()
    {
        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreateManagerRequest>(),
                CancellationToken.None
            ))
            .ReturnsAsync(new HandlerResponse<CreateManagerResponse>(
                isSuccess: false,
                httpStatusCode: HttpStatusCode.BadGateway,
                appStatusCode: AppStatusCode.Empty,
                errors: ManagerMessages.InvalidUsernameOrPassword
            ));

        var result = await _handler.Handle(
            request: _request,
            CancellationToken.None
        );

        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.NotNull(result.Errors);
        Assert.NotEmpty(result.Errors);
    }

    # endregion
}
