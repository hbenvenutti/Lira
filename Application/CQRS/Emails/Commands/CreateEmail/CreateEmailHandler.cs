using System.Net;
using Lira.Application.Dto;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Application.Specifications.Emails;
using Lira.Domain.Domains.Emails;
using Lira.Domain.Domains.Person;
using MediatR;

namespace Lira.Application.CQRS.Emails.Commands.CreateEmail;

public class CreateEmailHandler :
    IRequestHandler<CreateEmailRequest, Response<CreateEmailResponse>>
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

    public async Task<Response<CreateEmailResponse>> Handle(
        CreateEmailRequest request,
        CancellationToken cancellationToken
    )
    {
        # region ---- specification --------------------------------------------

        var specification = new EmailSpecification();

        var emailData = new EmailSpecificationDto(request.Address);

        if (!specification.IsSatisfiedBy(emailData))
        {
            return new Response<CreateEmailResponse>(
                httpStatusCode: HttpStatusCode.BadRequest,
                statusCode: specification.StatusCode,
                error: new ErrorDto(specification.ErrorMessages)
            );
        }

        # endregion

        # region ---- person ---------------------------------------------------

        if (!request.ValidatePerson) { goto email; }

        var person = await _personRepository.FindByIdAsync(request.PersonId);

        if (person is null)
        {
            return new Response<CreateEmailResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                statusCode: StatusCode.PersonNotFound,
                error: new ErrorDto(message: NotFoundMessages.PersonNotFound)
            );
        }

        # endregion

        # region ---- email ----------------------------------------------------

        email:

        var email = await _emailRepository.FindByAddressAsync(request.Address);

        if (email is not null)
        {
            return new Response<CreateEmailResponse>(
                httpStatusCode: HttpStatusCode.Conflict,
                statusCode: StatusCode.EmailAlreadyExists,
                error: new ErrorDto(message: ConflictMessages.EmailIsInUse)
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

        return new Response<CreateEmailResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.Created,
            statusCode: StatusCode.CreatedOne,
            data: new CreateEmailResponse(email.Id)
        );

        # endregion
    }
}
