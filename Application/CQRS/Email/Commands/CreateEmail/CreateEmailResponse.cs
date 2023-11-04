namespace Lira.Application.CQRS.Email.Commands.CreateEmail;

public class CreateEmailResponse
{
    public Guid Id { get; init; }

    public CreateEmailResponse(Guid id)
    {
        Id = id;
    }
}
