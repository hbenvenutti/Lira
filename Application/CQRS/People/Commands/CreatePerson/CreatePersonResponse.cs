namespace Lira.Application.CQRS.People.Commands.CreatePerson;

public class CreatePersonResponse
{
    public Guid Id { get; init; }

    public CreatePersonResponse(Guid id)
    {
        Id = id;
    }
}
