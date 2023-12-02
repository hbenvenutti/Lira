using System.Net;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Domain.Domains.Orixa;
using Lira.Domain.Domains.Person;
using Lira.Domain.Domains.PersonOrixa;
using MediatR;

namespace Lira.Application.CQRS.PersonOrixa.Commands.CreatePersonOrixa;

public class CreatePersonOrixaHandler :
    IRequestHandler<CreatePersonOrixaRequest, IHandlerResponse<CreatePersonOrixaResponse>>
{
    # region ---- properties ---------------------------------------------------

    private readonly IPersonOrixaRepository _personOrixaRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IOrixaRepository _orixaRepository;

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreatePersonOrixaHandler(
        IPersonOrixaRepository personOrixaRepository,
        IPersonRepository personRepository,
        IOrixaRepository orixaRepository
    )
    {
        _personOrixaRepository = personOrixaRepository;
        _personRepository = personRepository;
        _orixaRepository = orixaRepository;
    }

    # endregion

    public async Task<IHandlerResponse<CreatePersonOrixaResponse>> Handle(
        CreatePersonOrixaRequest request,
        CancellationToken cancellationToken
    )
    {
        # region ---- person ---------------------------------------------------

        if (!request.ValidatePerson) { goto orixa; }

        var person = await _personRepository.FindByIdAsync(request.PersonId);

        if (person is null)
        {
            return new HandlerResponse<CreatePersonOrixaResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                appStatusCode: AppStatusCode.PersonNotFound,
                errors: NotFoundMessages.PersonNotFound
            );
        }

        # endregion

        # region ---- orixa ----------------------------------------------------

        orixa:

        var orixa = await _orixaRepository.FindByIdAsync(request.OrixaId);

        if (orixa is null)
        {
            return new HandlerResponse<CreatePersonOrixaResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                appStatusCode: AppStatusCode.OrixaNotFound,
                errors: NotFoundMessages.OrixaNotFound
            );
        }

        # endregion

        # region ---- person orixa ---------------------------------------------

        var personOrixa = PersonOrixaDomain.Create(
            personId: request.PersonId,
            orixaId: request.OrixaId,
            type: request.Type
        );

        personOrixa = await _personOrixaRepository.CreateAsync(personOrixa);

        # endregion

        # region ---- response -------------------------------------------------

        return new HandlerResponse<CreatePersonOrixaResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.Created,
            appStatusCode: AppStatusCode.CreatedOne,
            data: new CreatePersonOrixaResponse(id: personOrixa.Id)
        );

        # endregion
    }
}
