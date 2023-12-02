using System.Net;
using Lira.Application.Responses;
using Lira.Common.Enums;
using Lira.Domain.Domains.Person;
using MediatR;

namespace Lira.Application.CQRS.People.Queries.GetPersonById;

public class GetPersonByIdHandler :
    IRequestHandler<GetPersonByIdRequest, IHandlerResponse<GetPersonByIdResponse>>
{
    private readonly IPersonRepository _personRepository;

    public GetPersonByIdHandler(
        IPersonRepository personRepository
    )
    {
        _personRepository = personRepository;
    }

    public async Task<IHandlerResponse<GetPersonByIdResponse>> Handle(
        GetPersonByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        var person = await _personRepository.FindByIdAsync(request.Id);

        if (person is null)
        {
            return new HandlerResponse<GetPersonByIdResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                appStatusCode: AppStatusCode.PersonNotFound,
                errors: PersonMessages.NotFound
            );
        }

        return new HandlerResponse<GetPersonByIdResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.OK,
            appStatusCode: AppStatusCode.FoundOne,
            data: new GetPersonByIdResponse(person.Id)
        );
    }
}
