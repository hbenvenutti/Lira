namespace Lira.Application.CQRS.PersonOrixa.Commands.CreatePersonOrixa;

public class CreatePersonOrixaResponse
{
    public Guid Id { get; init; }

    public CreatePersonOrixaResponse(Guid id)
    {
        Id = id;
    }
}
