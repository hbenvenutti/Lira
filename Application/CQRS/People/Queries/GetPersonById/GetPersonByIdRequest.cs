using Lira.Application.Responses;
using MediatR;

namespace Lira.Application.CQRS.People.Queries.GetPersonById;

public struct GetPersonByIdRequest :
    IRequest<IHandlerResponse<GetPersonByIdResponse>>
{
    public Guid Id { get; init; }

    public GetPersonByIdRequest(
        Guid id
    )
    {
        Id = id;
    }
}
