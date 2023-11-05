using System.Diagnostics.CodeAnalysis;
using System.Net;
using Lira.Application.CQRS.Address.Commands.CreateAddress;
using Lira.Application.CQRS.Emails.Commands.CreateEmail;
using Lira.Application.CQRS.Medium.Commands.CreateMedium;
using Lira.Application.CQRS.People.Commands.CreatePerson;
using Lira.Application.CQRS.People.Commands.RegisterPerson;
using Lira.Application.CQRS.PersonOrixa.Commands.CreatePersonOrixa;
using Lira.Application.CQRS.Phone.Commands.CreatePhone;
using Lira.Application.Dto;
using Lira.Application.Enums;
using Lira.Application.Responses;
using Lira.Domain.Enums;
using MediatR;
using Moq;

namespace Lira.Test.Commands.People;

[ExcludeFromCodeCoverage]
public class RegistrationTest
{
    private readonly Mock<IMediator> _mediatorMock;

    private readonly RegisterPersonHandler _handler;
    private readonly RegisterPersonRequest _request;

    # region ---- constants ----------------------------------------------------

    private static readonly Guid PersonId = Guid.NewGuid();

    private const string FirstName = "John";
    private const string Surname = "Doe";
    private const string Document = "12345678900";
    private const string Email = "johndoe@gmail.com";
    private const string PhoneNumber = "11999999999";
    private const string Street = "Rua dos Bobos";
    private const string Number = "0";
    private const string Neighborhood = "Centro";
    private const string City = "São Paulo";
    private const string State = "SP";
    private const string ZipCode = "94477453";
    private const string Complement = "Apto 123";
    private const bool IsMedium = true;
    private static readonly DateTime? FirstAmaci = DateTime.UtcNow;
    private static readonly DateTime? LastAmaci = DateTime.UtcNow;
    private static readonly Guid FrontOrixaId = Guid.NewGuid();
    private static readonly Guid AdjunctOrixaId = Guid.NewGuid();
    private static readonly Guid AncestralOrixaId = Guid.Empty;
    private static readonly EmailType? EmailType = 0;

    # endregion

    # region ---- constructor --------------------------------------------------

    public RegistrationTest()
    {
        _mediatorMock = new Mock<IMediator>();

        _handler = new RegisterPersonHandler(
            _mediatorMock.Object
        );

        SetupMediatorMock();

        _request = new RegisterPersonRequest(
            firstName: FirstName,
            surname: Surname,
            document: Document,
            email: Email,
            phoneNumber: PhoneNumber,
            street: Street,
            number: Number,
            neighborhood: Neighborhood,
            city: City,
            state: State,
            zipCode: ZipCode,
            complement: Complement,
            isMedium: IsMedium,
            firstAmaci: FirstAmaci,
            lastAmaci: LastAmaci,
            frontOrixaId: FrontOrixaId,
            adjunctOrixaId: AdjunctOrixaId,
            ancestralOrixaId: AncestralOrixaId,
            emailType: EmailType
        );
    }

    # endregion

    # region ---- mock setup ---------------------------------------------------

    private void SetupMediatorMock()
    {
        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreatePersonRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Response<CreatePersonResponse>(
                isSuccess: true,
                httpStatusCode: HttpStatusCode.Created,
                statusCode: StatusCode.CreatedOne,
                data: new CreatePersonResponse(
                    id: PersonId
                )
            ));

        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreateAddressRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Response<CreateAddressResponse>(
                isSuccess: true,
                httpStatusCode: HttpStatusCode.Created,
                statusCode: StatusCode.CreatedOne,
                data: new CreateAddressResponse(
                    id: Guid.NewGuid()
                )
            ));

        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreateEmailRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Response<CreateEmailResponse>(
                isSuccess: true,
                httpStatusCode: HttpStatusCode.Created,
                statusCode: StatusCode.CreatedOne,
                data: new CreateEmailResponse(
                    id: Guid.NewGuid()
                )
            ));

        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreatePhoneRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Response<CreatePhoneResponse>(
                isSuccess: true,
                httpStatusCode: HttpStatusCode.Created,
                statusCode: StatusCode.CreatedOne,
                data: new CreatePhoneResponse(
                    id: Guid.NewGuid()
                )
            ));

        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreatePersonOrixaRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Response<CreatePersonOrixaResponse>(
                isSuccess: true,
                httpStatusCode: HttpStatusCode.Created,
                statusCode: StatusCode.CreatedOne,
                data: new CreatePersonOrixaResponse(
                    id: Guid.NewGuid()
                )
            ));

        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreateMediumRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Response<CreateMediumResponse>(
                isSuccess: true,
                httpStatusCode: HttpStatusCode.Created,
                statusCode: StatusCode.CreatedOne,
                data: new CreateMediumResponse(
                    id: Guid.NewGuid()
                )
            ));
    }

    # endregion

    # region ---- register person ----------------------------------------------

    [Fact]
    public async void ShouldRegisterAPerson()
    {
        var result = await _handler.Handle(
            _request,
            CancellationToken.None
        );

        Assert.True(result.IsSuccess);
        Assert.Equal(
            expected: HttpStatusCode.Created,
            actual:result.HttpStatusCode
        );

        Assert.Equal(
            expected: StatusCode.CreatedTransaction,
            actual: result.StatusCode
        );

        Assert.NotNull(result.Data);

        Assert.Equal(
            expected: PersonId,
            actual: result.Data.Id
        );

        Assert.Null(result.Error);
        Assert.Null(result.Pagination);
    }

    # endregion

    # region ---- person service fails -----------------------------------------

    [Fact]
    public async void ShouldNotRegisterIfPersonFailsAsync()
    {
        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreatePersonRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Response<CreatePersonResponse>(
                isSuccess: false,
                httpStatusCode: HttpStatusCode.BadGateway,
                statusCode: StatusCode.Empty,
                error: new ErrorDto(message: string.Empty)
            ));

        var result = await _handler.Handle(
            _request,
            CancellationToken.None
        );

        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.Null(result.Pagination);
        Assert.NotNull(result.Error);
        Assert.NotNull(result.Error.Messages);
        Assert.NotEmpty(result.Error.Messages);
    }

    # endregion

    # region ---- address service fails ----------------------------------------

    [Fact]
    public async void ShouldNotRegisterIfAddressFailsAsync()
    {
        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreateAddressRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Response<CreateAddressResponse>(
                isSuccess: false,
                httpStatusCode: HttpStatusCode.BadGateway,
                statusCode: StatusCode.Empty,
                error: new ErrorDto(message: string.Empty)
            ));

        var result = await _handler.Handle(
            _request,
            CancellationToken.None
        );

        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.Null(result.Pagination);
        Assert.NotNull(result.Error);
        Assert.NotNull(result.Error.Messages);
        Assert.NotEmpty(result.Error.Messages);
    }

    # endregion

    # region ---- email service fails ------------------------------------------

    [Fact]
    public async void ShouldNotRegisterIfEmailFailsAsync()
    {
        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreateEmailRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Response<CreateEmailResponse>(
                isSuccess: false,
                httpStatusCode: HttpStatusCode.BadGateway,
                statusCode: StatusCode.Empty,
                error: new ErrorDto(message: string.Empty)
            ));

        var result = await _handler.Handle(
            _request,
            CancellationToken.None
        );

        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.Null(result.Pagination);
        Assert.NotNull(result.Error);
        Assert.NotNull(result.Error.Messages);
        Assert.NotEmpty(result.Error.Messages);
    }

    # endregion

    # region ---- phone service fails ------------------------------------------

    [Fact]
    public async void ShouldNotRegisterIfPhoneFailsAsync()
    {
        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreatePhoneRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Response<CreatePhoneResponse>(
                isSuccess: false,
                httpStatusCode: HttpStatusCode.BadGateway,
                statusCode: StatusCode.Empty,
                error: new ErrorDto(message: string.Empty)
            ));

        var result = await _handler.Handle(
            _request,
            CancellationToken.None
        );

        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.Null(result.Pagination);
        Assert.NotNull(result.Error);
        Assert.NotNull(result.Error.Messages);
        Assert.NotEmpty(result.Error.Messages);
    }

    # endregion

    # region ---- medium service fails ------------------------------------------

    [Fact]
    public async void ShouldNotRegisterIfMediumFailsAsync()
    {
        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreateMediumRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Response<CreateMediumResponse>(
                isSuccess: false,
                httpStatusCode: HttpStatusCode.BadGateway,
                statusCode: StatusCode.Empty,
                error: new ErrorDto(message: string.Empty)
            ));

        var result = await _handler.Handle(
            _request,
            CancellationToken.None
        );

        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.Null(result.Pagination);
        Assert.NotNull(result.Error);
        Assert.NotNull(result.Error.Messages);
        Assert.NotEmpty(result.Error.Messages);
    }

    # endregion

    # region ---- adjunct orixa service fails ----------------------------------

    [Theory]
    [InlineData(true, false, false)]
    [InlineData(false, true, false)]
    [InlineData(false, false, true)]
    public async void ShouldNotRegisterIfAdjunctFailsAsync(
        bool adjunctOrixaId,
        bool ancestralOrixaId,
        bool frontOrixaId
    )
    {
        var request = new RegisterPersonRequest(
            firstName: FirstName,
            surname: Surname,
            document: Document,
            email: Email,
            phoneNumber: PhoneNumber,
            street: Street,
            number: Number,
            neighborhood: Neighborhood,
            city: City,
            state: State,
            zipCode: ZipCode,
            complement: Complement,
            isMedium: IsMedium,
            firstAmaci: FirstAmaci,
            lastAmaci: LastAmaci,
            frontOrixaId: frontOrixaId ? FrontOrixaId : null,
            adjunctOrixaId: adjunctOrixaId ? AdjunctOrixaId : null,
            ancestralOrixaId: ancestralOrixaId ? AncestralOrixaId : null,
            emailType: EmailType
        );

        _mediatorMock
            .Setup(mediator => mediator.Send(
                It.IsAny<CreatePersonOrixaRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new Response<CreatePersonOrixaResponse>(
                isSuccess: false,
                httpStatusCode: HttpStatusCode.BadGateway,
                statusCode: StatusCode.Empty,
                error: new ErrorDto(message: string.Empty)
            ));

        var result = await _handler.Handle(
            request,
            CancellationToken.None
        );

        Assert.False(result.IsSuccess);
        Assert.Null(result.Data);
        Assert.Null(result.Pagination);
        Assert.NotNull(result.Error);
        Assert.NotNull(result.Error.Messages);
        Assert.NotEmpty(result.Error.Messages);
    }

    # endregion
}
