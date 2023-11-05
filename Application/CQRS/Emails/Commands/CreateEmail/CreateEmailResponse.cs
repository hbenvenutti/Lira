namespace Lira.Application.CQRS.Emails.Commands.CreateEmail;

public class CreateEmailResponse
{
    public Guid Id { get; init; }

    public CreateEmailResponse(Guid id)
    {
        Id = id;
    }
}
