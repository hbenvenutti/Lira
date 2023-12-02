using System.Diagnostics.CodeAnalysis;
using System.Net;
using BrazilianTypes.Types;
using Lira.Application.CQRS.People.Queries.GetPersonById;
using Lira.Application.Responses;
using Lira.Common.Enums;
using Lira.Domain.Domains.Person;
using Moq;

namespace Lira.Test.Commands.People;

[ExcludeFromCodeCoverage]
public class GetPersonByIdTest
{
    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly GetPersonByIdHandler _handler;
    private readonly Guid _personId = Guid.NewGuid();

    public GetPersonByIdTest()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _handler = new GetPersonByIdHandler(_personRepositoryMock.Object);
    }

    # region ---- success ------------------------------------------------------

    [Fact]
    public async Task Handle_WhenPersonExists_ReturnsSuccessResponse()
    {
        _personRepositoryMock
            .Setup(repo => repo.FindByIdAsync(
                _personId,
                false,
                false,
                false,
                false,
                false,
                false,
                false
            ))
            .ReturnsAsync(new PersonDomain(
                _personId,
                DateTime.UtcNow,
                Cpf.Generate(),
                "John",
                "Doe"
            ));

        var request = new GetPersonByIdRequest { Id = _personId };

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Null(result.Errors);
        Assert.IsType<HandlerResponse<GetPersonByIdResponse>>(result);

        Assert.Equal(
            expected: HttpStatusCode.OK,
            actual: result.HttpStatusCode
        );

        Assert.Equal(
            expected: AppStatusCode.FoundOne,
            actual: result.AppStatusCode
        );

        Assert.NotNull(result.Data);

        Assert.Equal(
            expected: _personId,
            actual: result.Data.Id
        );
    }

    # endregion

    # region ---- not found ----------------------------------------------------

    [Fact]
    public async Task Handle_WhenPersonDoesNotExist_ReturnsNotFoundResponse()
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
                )
            )
            .ReturnsAsync(null as PersonDomain);

        var request = new GetPersonByIdRequest { Id = _personId };

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsSuccess);
        Assert.IsType<HandlerResponse<GetPersonByIdResponse>>(result);
        Assert.Null(result.Data);

        Assert.Equal(
            expected: HttpStatusCode.NotFound,
            actual: result.HttpStatusCode
        );

        Assert.Equal(
            expected: AppStatusCode.PersonNotFound,
            actual: result.AppStatusCode
        );

        Assert.NotNull(result.Errors);
        Assert.NotEmpty(result.Errors);
        Assert.Single(result.Errors);
        Assert.Contains(
            expected: PersonMessages.NotFound,
            collection: result.Errors
        );
    }

    # endregion
}
