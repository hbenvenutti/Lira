using System.Diagnostics.CodeAnalysis;
using System.Net;
using BrazilianTypes.Types;
using Lira.Application.CQRS.Emails.Commands.CreateEmail;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Domain.Domains.Emails;
using Lira.Domain.Domains.Person;
using Lira.Domain.Enums;
using Moq;

namespace Lira.Test.Commands.Emails;

[ExcludeFromCodeCoverage]
public class CreateEmailTest
{
    # region ---- properties ---------------------------------------------------

    private readonly Mock<IEmailRepository> _emailRepository = new();
    private readonly Mock<IPersonRepository> _personRepository = new();

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
            _emailRepository.Object,
            _personRepository.Object
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

        _personRepository
            .Setup(repository => repository
                .FindByIdAsync(
                    It.IsAny<Guid>(),
                    false,
                    false,
                    false,
                    false,
                    false,
                    false,
                    false
                )
            )
            .ReturnsAsync(null as PersonDomain);
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
            expected: StatusCode.CreatedOne,
            actual: response.StatusCode
        );

        Assert.NotNull(response.Data);
        Assert.Equal(expected: EmailId, actual: response.Data.Id);
        Assert.Null(response.Error);
        Assert.Null(response.Pagination);
    }

    # endregion

    # region ---- person not found ---------------------------------------------

    [Fact]
    public async void ShouldReturnPersonNotFound()
    {
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

        Assert.Equal(
            expected: HttpStatusCode.NotFound,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: StatusCode.PersonNotFound,
            actual: response.StatusCode
        );

        Assert.NotNull(response.Error);
        Assert.NotNull(response.Error.Messages);
        Assert.NotEmpty(response.Error.Messages);
        Assert.Single(response.Error.Messages);

        Assert.Contains(
            expected: NotFoundMessages.PersonNotFound,
            collection: response.Error.Messages
        );

        Assert.Null(response.Data);
        Assert.Null(response.Pagination);
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
            expected: StatusCode.EmailAlreadyExists,
            actual: response.StatusCode
        );

        Assert.NotNull(response.Error);
        Assert.NotNull(response.Error.Messages);
        Assert.NotEmpty(response.Error.Messages);
        Assert.Single(response.Error.Messages);

        Assert.Contains(
            expected: ConflictMessages.EmailIsInUse,
            collection: response.Error.Messages
        );

        Assert.Null(response.Data);
        Assert.Null(response.Pagination);
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
            expected: StatusCode.InvalidEmailAddress,
            actual: response.StatusCode
        );

        Assert.NotNull(response.Error);
        Assert.NotNull(response.Error.Messages);
        Assert.NotEmpty(response.Error.Messages);
        Assert.Single(response.Error.Messages);

        Assert.Contains(
            expected: PersonMessages.InvalidEmail,
            collection: response.Error.Messages
        );

        Assert.Null(response.Data);
        Assert.Null(response.Pagination);
    }

    # endregion
}
