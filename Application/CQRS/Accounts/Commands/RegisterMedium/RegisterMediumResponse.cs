namespace Lira.Application.CQRS.Accounts.Commands.RegisterMedium;

public class RegisterMediumResponse
{
    public Guid Id { get; init; }
    public Guid PersonId { get; init; }

    public RegisterMediumResponse(Guid id, Guid personId)
    {
        Id = id;
        PersonId = personId;
    }
}
