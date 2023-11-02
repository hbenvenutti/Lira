namespace Lira.Application.CQRS.Accounts.Commands.RegisterMedium;

public class RegisterPersonResponse
{
    public Guid Id { get; init; }
    public Guid PersonId { get; init; }

    public RegisterPersonResponse(Guid id, Guid personId)
    {
        Id = id;
        PersonId = personId;
    }
}
