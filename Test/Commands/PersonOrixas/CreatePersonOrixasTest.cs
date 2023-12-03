using System.Diagnostics.CodeAnalysis;
using System.Net;
using BrazilianTypes.Types;
using Lira.Application.CQRS.PersonOrixa.Commands.CreatePersonOrixa;
using Lira.Application.Messages;
using Lira.Common.Enums;
using Lira.Domain.Domains.Orixa;
using Lira.Domain.Domains.Person;
using Lira.Domain.Domains.PersonOrixa;
using Lira.Domain.Enums;
using Moq;

namespace Lira.Test.Commands.PersonOrixas;

[ExcludeFromCodeCoverage]
public class CreatePersonOrixasTest
{
    # region ---- properties ---------------------------------------------------

    private readonly Mock<IPersonOrixaRepository> _personOrixaRepository;
    private readonly Mock<IPersonRepository> _personRepository;
    private readonly Mock<IOrixaRepository> _orixaRepository;

    private readonly CreatePersonOrixaHandler _handler;
    private readonly CreatePersonOrixaRequest _request;


    private static readonly Guid PersonOrixaId = Guid.NewGuid();
    private static readonly Guid PersonId = Guid.NewGuid();
    private static readonly Guid OrixaId = Guid.NewGuid();

    private static readonly Name OrixaName = "Oxalá";

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreatePersonOrixasTest()
    {
        _personOrixaRepository = new Mock<IPersonOrixaRepository>();
        _personRepository = new Mock<IPersonRepository>();
        _orixaRepository = new Mock<IOrixaRepository>();

        SetupMocks();

        _handler = new CreatePersonOrixaHandler(
            _personOrixaRepository.Object,
            _personRepository.Object,
            _orixaRepository.Object
        );

        _request = new CreatePersonOrixaRequest(
            personId: PersonId,
            orixaId: OrixaId,
            type: OrixaType.Adjunct,
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

        _orixaRepository
            .Setup(repository => repository
                .FindByIdAsync(
                    It.IsAny<Guid>()
                )
            )
            .ReturnsAsync(new OrixaDomain(
                id: OrixaId,
                name: OrixaName,
                createdAt: DateTime.UtcNow
            ));

        _personOrixaRepository
            .Setup(repository => repository
                .CreateAsync(
                    It.IsAny<PersonOrixaDomain>()
                )
            )
            .ReturnsAsync(new PersonOrixaDomain(
                id: PersonOrixaId,
                personId: PersonId,
                orixaId: OrixaId,
                type: OrixaType.Adjunct,
                createdAt: DateTime.UtcNow
            ));
    }

    # endregion

    # region ---- success ------------------------------------------------------

    [Fact]
    public async Task ShouldCreatePersonOrixa()
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

        Assert.Equal(
            expected: PersonOrixaId,
            actual: response.Data.Id
        );
    }

    # endregion

    # region ---- person not found ---------------------------------------------

    [Fact]
    public async Task ShouldReturnPersonNotFound()
    {
        var request = new CreatePersonOrixaRequest(
            personId: PersonId,
            orixaId: OrixaId,
            type: OrixaType.Adjunct,
            validatePerson: true
        );

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
    }

    # endregion

    # region ---- orixa not found ----------------------------------------------

    [Fact]
    public async Task ShouldReturnOrixaNotFound()
    {
        _orixaRepository
            .Setup(repository => repository
                .FindByIdAsync(
                    It.IsAny<Guid>()
                )
            )
            .ReturnsAsync(null as OrixaDomain);

        var response = await _handler.Handle(_request, CancellationToken.None);

        Assert.False(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.NotFound,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: AppStatusCode.OrixaNotFound,
            actual: response.AppStatusCode
        );

        Assert.NotNull(response.Errors);

        Assert.NotEmpty(response.Errors);
        Assert.Single(response.Errors);

        Assert.Contains(
            expected: NotFoundMessages.OrixaNotFound,
            collection: response.Errors
        );

        Assert.Null(response.Data);
    }

    # endregion
}
