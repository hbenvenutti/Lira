using System.Diagnostics.CodeAnalysis;
using System.Net;
using BrazilianTypes.Types;
using Lira.Application.CQRS.Phone.Commands.CreatePhone;
using Lira.Application.Messages;
using Lira.Common.Enums;
using Lira.Domain.Domains.Person;
using Lira.Domain.Domains.Phones;
using Moq;

namespace Lira.Test.Commands.Phones;

[ExcludeFromCodeCoverage]
public class CreatePhoneHandlerTest
{
    # region ---- properties ---------------------------------------------------

    private readonly Mock<IPhoneRepository> _phoneRepository = new();
    private readonly Mock<IPersonRepository> _personRepository = new();

    private readonly CreatePhoneHandler _handler;
    private readonly CreatePhoneRequest _request;

    private static readonly Phone Phone = "51 99999-9999";
    private static readonly Guid PersonId = Guid.NewGuid();
    private static readonly Guid PhoneId = Guid.NewGuid();
    private const string InvalidPhone = "";

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreatePhoneHandlerTest()
    {
        SetupMocks();

        _handler = new CreatePhoneHandler(
            _phoneRepository.Object,
            _personRepository.Object
        );

        _request = new CreatePhoneRequest(
            phoneNumber: Phone,
            personId: PersonId,
            validatePerson: false
        );
    }

    # endregion

    # region ---- setup mocks --------------------------------------------------

    private void SetupMocks()
    {
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

        _phoneRepository
            .Setup(repository => repository
                .CreateAsync(It.IsAny<PhoneDomain>())
            )
            .ReturnsAsync(
                new PhoneDomain(
                    id: PhoneId,
                    phone: Phone,
                    personId: PersonId,
                    createdAt: DateTime.UtcNow
                )
            );
    }

    # endregion

    # region ---- success ------------------------------------------------------

    [Fact]
    public async Task ShouldCreatePhone()
    {
        var response = await _handler.Handle(_request, CancellationToken.None);

        Assert.True(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.Created,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: AppStatusCode.CreatedOne,
            actual: response.AppStatusCode
        );

        Assert.Null(response.Errors);

        Assert.NotNull(response.Data);
        Assert.Equal(expected: PhoneId, actual: response.Data.Id);
    }

    # endregion

    # region ---- specification ------------------------------------------------

    [Fact]
    public async Task ShouldReturnBadRequestWhenSpecificationIsNotSatisfied()
    {
        var request = new CreatePhoneRequest(
            phoneNumber: InvalidPhone,
            personId: PersonId,
            validatePerson: false
        );

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.BadRequest,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: AppStatusCode.InvalidPhone,
            actual: response.AppStatusCode
        );

        Assert.NotNull(response.Errors);
        Assert.NotEmpty(response.Errors);

        Assert.Null(response.Data);
    }

    # endregion

    # region ---- person not found ---------------------------------------------

    [Fact]
    public async Task ShouldReturnPersonNotFound()
    {
        var request = new CreatePhoneRequest(
            phoneNumber: Phone,
            personId: PersonId,
            validatePerson: true
        );

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.NotFound,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: AppStatusCode.PersonNotFound,
            actual: response.AppStatusCode
        );

        Assert.NotNull(response.Errors);
        Assert.NotEmpty(response.Errors);
        Assert.Single(response.Errors);

        Assert.Contains(
            expected: NotFoundMessages.PersonNotFound,
            collection: response.Errors
        );

        Assert.Null(response.Data);
    }

    # endregion
}
