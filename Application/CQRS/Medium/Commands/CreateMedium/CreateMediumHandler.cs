using System.Net;
using Lira.Application.CQRS.People.Queries.GetPersonById;
using Lira.Application.Responses;
using Lira.Common.Enums;
using Lira.Domain.Domains.Medium;
using MediatR;

namespace Lira.Application.CQRS.Medium.Commands.CreateMedium;

public class CreateMediumHandler :
    IRequestHandler<CreateMediumRequest, IHandlerResponse<CreateMediumResponse>>
{
    # region ---- properties ---------------------------------------------------

    private readonly IMediator _mediator;
    private readonly IMediumRepository _mediumRepository;

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateMediumHandler(
        IMediator mediator,
        IMediumRepository mediumRepository
    )
    {
        _mediator = mediator;
        _mediumRepository = mediumRepository;
    }

    # endregion

    public async Task<IHandlerResponse<CreateMediumResponse>> Handle(
        CreateMediumRequest request,
        CancellationToken cancellationToken
    )
    {
        # region ---- person ---------------------------------------------------

        if (request.ValidatePerson)
        {

            var personRequest = new GetPersonByIdRequest(
                request.PersonId
            );

            var personResult = await _mediator.Send(
                personRequest,
                cancellationToken
            );

            if (!personResult.IsSuccess)
            {
                return new HandlerResponse<CreateMediumResponse>(
                    httpStatusCode: personResult.HttpStatusCode,
                    appStatusCode: personResult.AppStatusCode,
                    errors: personResult.Errors ?? new List<string>()
                );
            }
        }

        # endregion

        # region ---- medium ---------------------------------------------------

        var medium = MediumDomain.Create(
            personId: request.PersonId,
            firstAmaci: request.FirstAmaci,
            lastAmaci: request.LastAmaci
        );

        medium = await _mediumRepository.CreateAsync(medium);

        # endregion

        # region ---- response -------------------------------------------------

        return new HandlerResponse<CreateMediumResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.Created,
            appStatusCode: AppStatusCode.CreatedOne,
            data: new CreateMediumResponse(id: medium.Id)
        );

        # endregion
    }
}
