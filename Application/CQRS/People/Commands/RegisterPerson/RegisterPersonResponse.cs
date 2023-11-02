namespace Lira.Application.CQRS.Accounts.Commands.RegisterMedium;

public class RegisterPersonResponse
{
    public Guid Id { get; init; }

    public RegisterPersonResponse(Guid id)
    {
        Id = id;
    }
}
