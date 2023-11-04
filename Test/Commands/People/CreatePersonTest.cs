using System.Diagnostics.CodeAnalysis;
using System.Net;
using BrazilianTypes.Types;
using Lira.Application.CQRS.People.Commands.CreatePerson;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Domain.Domains.Person;
using Moq;

namespace Lira.Test.Commands.People;

[ExcludeFromCodeCoverage]
public class CreatePersonTest
{
    # region ---- properties ---------------------------------------------------

    private readonly Mock<IPersonRepository> _personRepositoryMock;
    private readonly CreatePersonHandler _handler;
    private CreatePersonRequest _request;

    private static readonly Cpf Cpf = Cpf.Generate();
    private static readonly Guid PersonId = Guid.NewGuid();
    private const string Name = "john";
    private const string Surname = "doe";

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreatePersonTest()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();

        SetupMocks();

        _request = new CreatePersonRequest(
            firstName: Name,
            surname: Surname,
            document: Cpf
        );

        _handler = new CreatePersonHandler(
            _personRepositoryMock.Object
        );
    }

    # endregion

    # region ---- setup mocks --------------------------------------------------

    private void SetupMocks()
    {
        _personRepositoryMock
            .Setup(repo => repo.CreateAsync(
                It.IsAny<PersonDomain>()
            ))
            .ReturnsAsync(new PersonDomain(
                id: PersonId,
                name: Name,
                surname: Surname,
                cpf: Cpf,
                createdAt: DateTime.UtcNow
            ));

        _personRepositoryMock
            .Setup(repo => repo.FindByCpfAsync(
                It.IsAny<Cpf>(),
                false,
                false,
                false,
                false,
                false,
                false,
                false
            ))
            .ReturnsAsync(null as PersonDomain);

        // _personRepositoryMock
        //     .Setup(repo => repo.FindByCpfAsync(
        //         Cpf,
        //         false,
        //         false,
        //         false,
        //         false,
        //         false,
        //         false,
        //         false
        //     ))
        //     .ReturnsAsync(new PersonDomain(
        //         id: Guid.NewGuid(),
        //         name: Name,
        //         surname: Surname,
        //         cpf: Cpf,
        //         createdAt: DateTime.UtcNow
        //     ));
    }

    # endregion

    # region ---- success ------------------------------------------------------

    [Fact]
    public async Task ShouldCreatePersonAsync()
    {
        var response = await _handler.Handle(
            _request,
            CancellationToken.None
        );

        Assert.True(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.Created,
            actual: response.HttpStatusCode)
        ;

        Assert.Equal(
            expected: StatusCode.CreatedOne,
            actual: response.StatusCode
        );

        Assert.Equal(
            expected: PersonId,
            actual: response.Data?.Id
        );
    }

    # endregion

    # region ---- test person exists -------------------------------------------

    [Fact]
    public async Task ShouldReturnConflictWhenPersonExistsAsync()
    {
        _personRepositoryMock
            .Setup(repo => repo.FindByCpfAsync(
                It.IsAny<Cpf>(),
                false,
                false,
                false,
                false,
                false,
                false,
                false
            ))
            .ReturnsAsync(new PersonDomain(
                id: Guid.NewGuid(),
                name: Name,
                surname: Surname,
                cpf: Cpf,
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
            expected: StatusCode.PersonAlreadyExists,
            actual: response.StatusCode
        );

        Assert.Equal(
            expected: PersonMessages.PersonAlreadyExists,
            actual: response.Error?.Messages.FirstOrDefault()
        );
    }

    # endregion

    # region ---- test specification -------------------------------------------

    [Fact]
    public async Task ShouldReturnBadRequestWhenSpecificationFailsAsync()
    {
        _request = new CreatePersonRequest(
            firstName: string.Empty,
            surname: string.Empty,
            document: Cpf
        );

        var response = await _handler.Handle(
            _request,
            CancellationToken.None
        );

        Assert.False(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.BadRequest,
            actual: response.HttpStatusCode
        );
    }

    # endregion
}