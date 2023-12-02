namespace Lira.Application.CQRS.People.Queries.GetPersonById;

public readonly struct GetPersonByIdResponse
{
    public Guid Id { get; init; }

    public GetPersonByIdResponse(Guid id)
    {
        Id = id;
    }
}
