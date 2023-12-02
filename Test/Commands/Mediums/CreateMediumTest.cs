using System.Diagnostics.CodeAnalysis;
using System.Net;
using Lira.Application.CQRS.Medium.Commands.CreateMedium;
using Lira.Application.CQRS.People.Commands.CreatePerson;
using Lira.Application.CQRS.People.Queries.GetPersonById;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Common.Enums;
using Lira.Domain.Domains.Medium;
using Lira.Domain.Domains.Person;
using MediatR;
using Moq;

namespace Lira.Test.Commands.Mediums;

[ExcludeFromCodeCoverage]
public class CreateMediumTest
{
    # region ---- properties ---------------------------------------------------

    private readonly Mock<IMediator> _mediator = new();
    private readonly Mock<IMediumRepository> _mediumRepository;

    private readonly CreateMediumHandler _handler;
    private readonly CreateMediumRequest _request;

    private static readonly Guid PersonId = Guid.NewGuid();
    private static readonly Guid MediumId = Guid.NewGuid();

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateMediumTest()
    {
        _mediumRepository = new Mock<IMediumRepository>();

        SetupMocks();

        _handler = new CreateMediumHandler(
            _mediator.Object,
            _mediumRepository.Object
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
        _mediator
            .Setup(mediator => mediator.Send(
                It.IsAny<GetPersonByIdRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new HandlerResponse<GetPersonByIdResponse>(
                isSuccess: true,
                httpStatusCode: HttpStatusCode.OK,
                appStatusCode: AppStatusCode.FoundOne,
                data: new GetPersonByIdResponse(
                    id: PersonId
                )
            ));

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
        _mediator
            .Setup(mediator => mediator.Send(
                It.IsAny<GetPersonByIdRequest>(),
                It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(new HandlerResponse<GetPersonByIdResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                appStatusCode: AppStatusCode.PersonNotFound,
                errors: new List<string>()
            ));

        var request = new CreateMediumRequest(
            personId: PersonId,
            validatePerson: true
        );

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
    }

    # endregion
}
