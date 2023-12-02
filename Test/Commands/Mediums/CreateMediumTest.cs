using System.Diagnostics.CodeAnalysis;
using System.Net;
using Lira.Application.CQRS.Medium.Commands.CreateMedium;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Domain.Domains.Medium;
using Lira.Domain.Domains.Person;
using Moq;

namespace Lira.Test.Commands.Mediums;

[ExcludeFromCodeCoverage]
public class CreateMediumTest
{
    # region ---- properties ---------------------------------------------------

    private readonly Mock<IMediumRepository> _mediumRepository;
    private readonly Mock<IPersonRepository> _personRepository;

    private readonly CreateMediumHandler _handler;
    private readonly CreateMediumRequest _request;

    private static readonly Guid PersonId = Guid.NewGuid();
    private static readonly Guid MediumId = Guid.NewGuid();

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateMediumTest()
    {
        _mediumRepository = new Mock<IMediumRepository>();
        _personRepository = new Mock<IPersonRepository>();

        SetupMocks();

        _handler = new CreateMediumHandler(
            _mediumRepository.Object,
            _personRepository.Object
        );

        _request = new CreateMediumRequest(
            personId: PersonId,
            validatePerson: false
        );
    }

    # endregion

    # region ---- setup --------------------------------------------------------

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

        _mediumRepository
            .Setup(repository => repository
                .CreateAsync(It.IsAny<MediumDomain>())
            )
            .ReturnsAsync(new MediumDomain(
                id: MediumId,
                personId: PersonId,
                firstAmaci: null,
                lastAmaci: null,
                createdAt: DateTime.UtcNow
            ));
    }

    # endregion

    # region ---- success ------------------------------------------------------

    [Fact]
    public async Task Success()
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
        Assert.Equal(expected: MediumId, actual: response.Data.Id);
    }

    # endregion

    # region ---- person not found ---------------------------------------------

    [Fact]
    public async Task PersonNotFound()
    {
        var request = new CreateMediumRequest(
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
