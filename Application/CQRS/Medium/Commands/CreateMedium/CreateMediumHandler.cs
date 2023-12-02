using System.Net;
using Lira.Application.Responses;
using Lira.Common.Enums;
using Lira.Domain.Domains.Medium;
using Lira.Domain.Domains.Person;
using MediatR;

namespace Lira.Application.CQRS.Medium.Commands.CreateMedium;

public class CreateMediumHandler :
    IRequestHandler<CreateMediumRequest, IHandlerResponse<CreateMediumResponse>>
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

    public async Task<IHandlerResponse<CreateMediumResponse>> Handle(
        CreateMediumRequest request,
        CancellationToken cancellationToken
    )
    {
        if (!request.ValidatePerson) { goto medium; }

        # region ---- person ---------------------------------------------------

        var person = await _personRepository.FindByIdAsync(request.PersonId);

        if (person is null)
        {
            return new HandlerResponse<CreateMediumResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                appStatusCode: AppStatusCode.PersonNotFound,
                errors: PersonMessages.NotFound
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

        return new HandlerResponse<CreateMediumResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.Created,
            appStatusCode: AppStatusCode.CreatedOne,
            data: new CreateMediumResponse(id: medium.Id)
        );

        # endregion
    }
}
