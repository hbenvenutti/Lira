namespace Lira.Application.CQRS.Address.Commands.CreateAddress;

public class CreateAddressResponse
{
    public Guid Id { get; init; }

    public CreateAddressResponse(Guid id)
    {
        Id = id;
    }
}
