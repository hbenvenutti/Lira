using System.Net;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Common.Enums;
using Lira.Domain.Domains.Emails;
using Lira.Domain.Domains.Person;
using MediatR;

namespace Lira.Application.CQRS.Emails.Commands.CreateEmail;

public class CreateEmailHandler :
    IRequestHandler<CreateEmailRequest, IHandlerResponse<CreateEmailResponse>>
{
    # region ---- repositories -------------------------------------------------

    private readonly IEmailRepository _emailRepository;
    private readonly IPersonRepository _personRepository;

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateEmailHandler(
        IEmailRepository emailRepository,
        IPersonRepository personRepository
    )
    {
        _emailRepository = emailRepository;
        _personRepository = personRepository;
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

        if (!request.ValidatePerson) { goto email; }

        var person = await _personRepository.FindByIdAsync(request.PersonId);

        if (person is null)
        {
            return new HandlerResponse<CreateEmailResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                appStatusCode: AppStatusCode.PersonNotFound,
                errors: PersonMessages.NotFound
            );
        }

        # endregion

        # region ---- email ----------------------------------------------------

        email:

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
