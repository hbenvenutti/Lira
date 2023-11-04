namespace Lira.Application.CQRS.Managers.Commands.CreateManager;

public class CreateManagerResponse
{
    public Guid Id { get; init; }

    public CreateManagerResponse(Guid id)
    {
        Id = id;
    }
}
