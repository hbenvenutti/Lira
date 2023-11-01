using System.Diagnostics.CodeAnalysis;
using System.Net;
using BrazilianTypes.Types;
using Lira.Application.CQRS.Managers.Commands.CreateAdmin;
using Lira.Application.Enums;
using Lira.Domain.Domains.Manager;
using Lira.Domain.Domains.Person;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Lira.Test;

[ExcludeFromCodeCoverage]
public class CreateAdminTest
{
    [Theory]
    [InlineData(
        "john",
        "doe",
        "854.247.400-73",
        "validCode",
        "A1234567",
        "jdoe"
    )]
    [InlineData(
        " john ",
        " doe ",
        "854.247.40073",
        "validCode",
        "A1234567",
        " jdoe "
    )]
    [InlineData(
        "john foo",
        "doe bar",
        "85424740073",
        "validCode",
        "A1234567",
        " jdoe "
    )]
    public async void ShouldCreateAdmin(
        string name,
        string surname,
        string cpf,
        string code,
        string password,
        string username
    )
    {
        # region ---- arrange --------------------------------------------------

        var configurationMock = new Mock<IConfiguration>();

        var personId = Guid.NewGuid();
        var managerId = Guid.NewGuid();

        configurationMock
            .Setup(config => config["Admin:Code"])
            .Returns("validCode");

        var personRepositoryMock = new Mock<IPersonRepository>();

        personRepositoryMock
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

        personRepositoryMock
            .Setup(repo => repo.CreateAsync(
                It.IsAny<PersonDomain>()
            ))
            .ReturnsAsync(new PersonDomain(
                id: personId,
                name: name,
                surname: surname,
                cpf: cpf,
                createdAt: DateTime.UtcNow
            ));

        var managerRepositoryMock = new Mock<IManagerRepository>();

        managerRepositoryMock
            .Setup(repo => repo.FindAllAsync(
                false,
                false
            ))
            .ReturnsAsync(new List<ManagerDomain>());

        managerRepositoryMock
            .Setup(repo => repo.CreateAsync(
                It.IsAny<ManagerDomain>()
            ))
            .ReturnsAsync(new ManagerDomain(
                id: managerId,
                username: username,
                password: password,
                personId: personId,
                createdAt: DateTime.UtcNow
            ));

        var handler = new CreateAdminHandler(
            configurationMock.Object,
            personRepositoryMock.Object,
            managerRepositoryMock.Object
        );

        var request = new CreateAdminRequest(
            name: name,
            surname: surname,
            cpf: cpf,
            code: code,
            password: password,
            passwordConfirmation: password,
            username: username
        );

        # endregion

        # region ---- act ------------------------------------------------------

        var response = await handler.Handle(
            request: request,
            CancellationToken.None
        );

        # endregion

        # region ---- assert ---------------------------------------------------

        Assert.True(response.IsSuccess);

        Assert.Equal(
            expected: HttpStatusCode.Created,
            actual: response.HttpStatusCode
        );

        Assert.Equal(
            expected: StatusCode.CreatedTransaction,
            actual: response.StatusCode
        );

        Assert.Null(response.Error);
        Assert.Null(response.Pagination);

        Assert.NotNull(response.Data);

        Assert.Equal(
            expected: managerId,
            actual: response.Data.Id
        );

        # endregion
    }
}
