namespace Lira.Application.CQRS.Phone.Commands.CreatePhone;

public class CreatePhoneResponse
{
    public Guid Id { get; init; }

    public CreatePhoneResponse(Guid id)
    {
        Id = id;
    }
}
