namespace Lira.Application.CQRS.People.Queries.GetPersonById;

public class GetPersonByIdResponse
{
    public Guid Id { get; init; }

    public GetPersonByIdResponse(Guid id)
    {
        Id = id;
    }
}
