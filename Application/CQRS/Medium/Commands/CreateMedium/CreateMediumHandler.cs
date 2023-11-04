using System.Net;
using Lira.Application.Dto;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Domain.Domains.Medium;
using Lira.Domain.Domains.Person;
using MediatR;

namespace Lira.Application.CQRS.Medium.Commands.CreateMedium;

public class CreateMediumHandler :
    IRequestHandler<CreateMediumRequest, Response<CreateMediumResponse>>
{
    # region ---- properties ---------------------------------------------------

    private readonly IMediumRepository _mediumRepository;
    private readonly IPersonRepository _personRepository;

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateMediumHandler(
        IMediumRepository mediumRepository,
        IPersonRepository personRepository
    )
    {
        _mediumRepository = mediumRepository;
        _personRepository = personRepository;
    }

    # endregion

    public async Task<Response<CreateMediumResponse>> Handle(
        CreateMediumRequest request,
        CancellationToken cancellationToken
    )
    {
        if (!request.ValidatePerson) { goto medium; }

        # region ---- person ---------------------------------------------------

        var person = await _personRepository.FindByIdAsync(request.PersonId);

        if (person is null)
        {
            return new Response<CreateMediumResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                statusCode: StatusCode.PersonNotFound,
                error: new ErrorDto( message: NotFoundMessages.PersonNotFound)
            );
        }

        # endregion

        # region ---- medium ---------------------------------------------------

        medium:

        var medium = MediumDomain.Create(
            personId: request.PersonId,
            firstAmaci: request.FirstAmaci,
            lastAmaci: request.LastAmaci
        );

        medium = await _mediumRepository.CreateAsync(medium);

        # endregion

        # region ---- response -------------------------------------------------

        return new Response<CreateMediumResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.Created,
            statusCode: StatusCode.CreatedOne,
            data: new CreateMediumResponse(id: medium.Id)
        );

        # endregion
    }
}