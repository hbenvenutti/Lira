using System.Diagnostics.CodeAnalysis;
using System.Net;
using BrazilianTypes.Types;
using Lira.Application.CQRS.Emails.Commands.CreateEmail;
using Lira.Application.CQRS.People.Queries.GetPersonById;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Common.Enums;
using Lira.Domain.Domains.Emails;
using Lira.Domain.Enums;
using MediatR;
using Moq;

namespace Lira.Test.Commands.Emails;

[ExcludeFromCodeCoverage]
public class CreateEmailTest
{
    # region ---- properties ---------------------------------------------------

    private readonly Mock<IMediator> _mediator = new();
    private readonly Mock<IEmailRepository> _emailRepository = new();

    private readonly CreateEmailHandler _handler;
    private readonly CreateEmailRequest _request;

    private static readonly Email Email = "foobar@gmail.com";
    private const string InvalidEmail = "";
    private static readonly Guid PersonId = Guid.NewGuid();
    private static readonly Guid EmailId = Guid.NewGuid();

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateEmailTest()
    {
        SetupMocks();

        _handler = new CreateEmailHandler(
            _mediator.Object,
            _emailRepository.Object
        );

        _request = new CreateEmailRequest(
            address:Email,
            personId: PersonId,
            type: EmailType.Personal,
            validatePerson: false
        );
    }

    # endregion

    # region ---- setup mocks --------------------------------------------------

    private void SetupMocks()
    {
        _mediator
            .Setup(mediator => mediator.Send(
                It.IsAny<GetPersonByIdRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new HandlerResponse<GetPersonByIdResponse>(
                isSuccess: true,
                httpStatusCode: HttpStatusCode.OK,
                appStatusCode: AppStatusCode.Empty
            ));

        _emailRepository
            .Setup(repository => repository
                .CreateAsync(It.IsAny<EmailDomain>())
            )
            .ReturnsAsync(new EmailDomain(
                id: EmailId,
                address: Email,
                type: EmailType.Personal,
                personId: PersonId,
                createdAt: DateTime.UtcNow
            ));
    }

    # endregion

    # region ---- success ------------------------------------------------------

    [Fact]
    public async void ShouldCreateEmail()
    {
        var response = await _handler.Handle(
            _request,
            CancellationToken.None
        );

        Assert.True(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.Created,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: AppStatusCode.CreatedOne,
            actual: response.AppStatusCode
        );

        Assert.NotNull(response.Data);
        Assert.Equal(expected: EmailId, actual: response.Data.Id);
        Assert.Null(response.Errors);
    }

    # endregion

    # region ---- person not found ---------------------------------------------

    [Fact]
    public async void ShouldReturnPersonNotFound()
    {
        _mediator
            .Setup(mediator => mediator.Send(
                It.IsAny<GetPersonByIdRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new HandlerResponse<GetPersonByIdResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                appStatusCode: AppStatusCode.Empty
            ));

        var request = new CreateEmailRequest(
            address: Email,
            personId: PersonId,
            type: EmailType.Personal,
            validatePerson: true
        );

        var response = await _handler.Handle(
            request,
            CancellationToken.None
        );

        Assert.False(response.IsSuccess);
    }

    # endregion

    # region ---- email already exists -----------------------------------------

    [Fact]
    public async void ShouldReturnEmailAlreadyExists()
    {
        _emailRepository
            .Setup(repository => repository
                .FindByAddressAsync(
                    It.IsAny<Email>(),
                    false
                )
            )
            .ReturnsAsync(new EmailDomain(
                id: EmailId,
                address: Email,
                type: EmailType.Personal,
                personId: PersonId,
                createdAt: DateTime.UtcNow
            ));

        var response = await _handler.Handle(
            _request,
            CancellationToken.None
        );

        Assert.False(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.Conflict,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: AppStatusCode.EmailAlreadyExists,
            actual: response.AppStatusCode
        );

        Assert.NotNull(response.Errors);
        Assert.NotEmpty(response.Errors);
        Assert.Single(response.Errors);

        Assert.Contains(
            expected: ConflictMessages.EmailIsInUse,
            collection: response.Errors
        );

        Assert.Null(response.Data);
    }

    # endregion

    # region ---- invalid email ------------------------------------------------

    [Fact]
    public async void ShouldReturnInvalidEmail()
    {
        var request = new CreateEmailRequest(
            address: InvalidEmail,
            personId: PersonId,
            type: EmailType.Personal,
            validatePerson: false
        );

        var response = await _handler.Handle(
            request,
            CancellationToken.None
        );

        Assert.False(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.BadRequest,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: AppStatusCode.InvalidEmailAddress,
            actual: response.AppStatusCode
        );

        Assert.NotNull(response.Errors);
        Assert.NotEmpty(response.Errors);
        Assert.Single(response.Errors);

        Assert.Contains(
            expected: EmailMessages.InvalidEmail,
            collection: response.Errors
        );

        Assert.Null(response.Data);
    }

    # endregion
}
