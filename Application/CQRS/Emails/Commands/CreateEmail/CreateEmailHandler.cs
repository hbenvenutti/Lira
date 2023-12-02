using System.Net;
using Lira.Application.CQRS.People.Queries.GetPersonById;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Common.Enums;
using Lira.Domain.Domains.Emails;
using MediatR;

namespace Lira.Application.CQRS.Emails.Commands.CreateEmail;

public class CreateEmailHandler :
    IRequestHandler<CreateEmailRequest, IHandlerResponse<CreateEmailResponse>>
{
    # region ---- repositories -------------------------------------------------

    private readonly IMediator _mediator;
    private readonly IEmailRepository _emailRepository;

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateEmailHandler(
        IMediator mediator,
        IEmailRepository emailRepository
    )
    {
        _mediator = mediator;
        _emailRepository = emailRepository;
    }

    # endregion

    public async Task<IHandlerResponse<CreateEmailResponse>> Handle(
        CreateEmailRequest request,
        CancellationToken cancellationToken
    )
    {
        # region ---- specification --------------------------------------------

        var specification = new EmailSpecification();

        var emailData = new EmailSpecificationDto(request.Address);

        if (!specification.IsSatisfiedBy(emailData))
        {
            return new HandlerResponse<CreateEmailResponse>(
                httpStatusCode: HttpStatusCode.BadRequest,
                appStatusCode: specification.AppStatusCode,
                errors: specification.ErrorMessages
            );
        }

        # endregion

        # region ---- person ---------------------------------------------------

        if (request.ValidatePerson)
        {
            var personRequest = new GetPersonByIdRequest(request.PersonId);
            var personResult = await _mediator.Send(
                personRequest,
                cancellationToken
            );

            if (!personResult.IsSuccess)
            {
                return new HandlerResponse<CreateEmailResponse>(
                    httpStatusCode: personResult.HttpStatusCode,
                    appStatusCode: personResult.AppStatusCode,
                    errors: personResult.Errors ?? new List<string>()
                );
            }
        }

        # endregion

        # region ---- email ----------------------------------------------------

        var email = await _emailRepository
            .FindByAddressAsync(request.Address);

        if (email is not null)
        {
            return new HandlerResponse<CreateEmailResponse>(
                httpStatusCode: HttpStatusCode.Conflict,
                appStatusCode: AppStatusCode.EmailAlreadyExists,
                errors:ConflictMessages.EmailIsInUse
            );
        }

        email = EmailDomain.Create(
            address: request.Address,
            type: request.Type,
            personId: request.PersonId
        );

        email = await _emailRepository.CreateAsync(email);

        # endregion

        # region ---- response -------------------------------------------------

        return new HandlerResponse<CreateEmailResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.Created,
            appStatusCode: AppStatusCode.CreatedOne,
            data: new CreateEmailResponse(email.Id)
        );

        # endregion
    }
}
