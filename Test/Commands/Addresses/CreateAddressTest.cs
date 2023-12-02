using System.Diagnostics.CodeAnalysis;
using System.Net;
using Lira.Application.CQRS.Address.Commands.CreateAddress;
using Lira.Common.Enums;
using Lira.Domain.Domains.Address;
using Lira.Domain.Domains.Person;
using Moq;

namespace Lira.Test.Commands.Addresses;

[ExcludeFromCodeCoverage]
public class CreateAddressTest
{
    # region ---- properties ---------------------------------------------------

    private readonly Mock<IAddressRepository> _addressRepository;
    private readonly Mock<IPersonRepository> _personRepository;
    private readonly CreateAddressHandler _handler;
    private readonly CreateAddressRequest _request;

    private static readonly Guid AddressId = Guid.NewGuid();
    private const string Street = "Rua dos Bobos";
    private const string Number = "0";
    private const string Neighborhood = "Centro";
    private const string City = "São Paulo";
    private const string State = "SP";
    private const string ZipCode = "00000-000";

    private static readonly Guid PersonId = Guid.NewGuid();

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateAddressTest()
    {
        _addressRepository = new Mock<IAddressRepository>();
        _personRepository = new Mock<IPersonRepository>();

        SetupMock();

        _handler = new CreateAddressHandler(
            _addressRepository.Object,
            _personRepository.Object
        );

        _request = new CreateAddressRequest(
            street: Street,
            number: Number,
            neighborhood: Neighborhood,
            city: City,
            state: State,
            zipCode: ZipCode,
            personId: PersonId,
            validatePerson: false
        );
    }

    # endregion

    # region ---- setup --------------------------------------------------------

    private void SetupMock()
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

        _addressRepository
            .Setup(repository => repository
                .CreateAsync(It.IsAny<AddressDomain>())
            )
            .ReturnsAsync(new AddressDomain(
                id: AddressId,
                street: Street,
                number: Number,
                neighborhood: Neighborhood,
                city: City,
                state: State,
                zipCode: ZipCode,
                personId: PersonId,
                createdAt: DateTime.Now
            ));
    }

    # endregion

    # region ---- success ------------------------------------------------------

    [Fact]
    public async void ShouldCreateAnAddressAsync()
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
            expected: AddressId,
            actual: response.Data.Id
        );
    }

    # endregion

    # region ---- person not found ---------------------------------------------

    [Fact]
    public async void ShouldReturnPersonNotFoundAsync()
    {
        var request = new CreateAddressRequest(
            street: Street,
            number: Number,
            neighborhood: Neighborhood,
            city: City,
            state: State,
            zipCode: ZipCode,
            personId: PersonId,
            validatePerson: true
        );

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
    }

    # endregion

    # region ---- specification ------------------------------------------------

    [Fact]
    public async void ShouldNotCreateIfSpecificationFails()
    {
        var request = new CreateAddressRequest(
            street: string.Empty,
            number: Number,
            neighborhood: Neighborhood,
            city: City,
            state: State,
            zipCode: ZipCode,
            personId: PersonId
        );

        var response = await _handler.Handle(request, CancellationToken.None);

        Assert.False(response.IsSuccess);
        Assert.Equal(
            expected: HttpStatusCode.BadRequest,
            actual: response.HttpStatusCode
        );
        Assert.Null(response.Data);
        Assert.NotNull(response.Errors);
        Assert.NotEmpty(response.Errors);
    }

    # endregion
}
