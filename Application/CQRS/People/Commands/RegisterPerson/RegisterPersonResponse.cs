namespace Lira.Application.CQRS.People.Commands.RegisterPerson;

public class RegisterPersonResponse
{
    public Guid Id { get; init; }

    public RegisterPersonResponse(Guid id)
    {
        Id = id;
    }
}
