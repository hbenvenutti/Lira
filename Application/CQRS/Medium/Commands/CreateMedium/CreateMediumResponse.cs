namespace Lira.Application.CQRS.Medium.Commands.CreateMedium;

public class CreateMediumResponse
{
    public Guid Id { get; set; }

    public CreateMediumResponse(Guid id)
    {
        Id = id;
    }
}
