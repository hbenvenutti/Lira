using System.Net;
using Lira.Application.Dto;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Application.Specifications.Phone;
using Lira.Domain.Domains.Person;
using Lira.Domain.Domains.Phone;
using MediatR;

namespace Lira.Application.CQRS.Phone.Commands;

public class CreatePhoneHandler
    : IRequestHandler<CreatePhoneRequest, Response<CreatePhoneResponse>>
{
    # region ---- properties ---------------------------------------------------

    private readonly IPhoneRepository _phoneRepository;
    private readonly IPersonRepository _personRepository;

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreatePhoneHandler(
        IPhoneRepository phoneRepository,
        IPersonRepository personRepository
    )
    {
        _phoneRepository = phoneRepository;
        _personRepository = personRepository;
    }

    # endregion

    public async Task<Response<CreatePhoneResponse>> Handle(
        CreatePhoneRequest request, CancellationToken cancellationToken
    )
    {
        # region ---- specification --------------------------------------------

        var specification = new PhoneSpecification();

        var specificationData = new PhoneSpecificationDto(request.PhoneNumber);

        if (!specification.IsSatisfiedBy(specificationData))
        {
            return new Response<CreatePhoneResponse>(
                httpStatusCode: HttpStatusCode.BadRequest,
                statusCode: specification.StatusCode,
                error: new ErrorDto(specification.ErrorMessages)
            );
        }

        # endregion

        # region ---- person validation ----------------------------------------

        var person = await _personRepository.FindByIdAsync(request.PersonId);

        if (person is null)
        {
            return new Response<CreatePhoneResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                statusCode: StatusCode.PersonNotFound,
                error: new ErrorDto(message: ResourceMessages.PersonNotFound)
            );
        }

        # endregion

        # region ---- phone ----------------------------------------------------

        var phone = await _phoneRepository.CreateAsync(
            PhoneDomain.Create(
                request.PhoneNumber,
                person.Id
            )
        );

        # endregion

        # region ---- response -------------------------------------------------

        return new Response<CreatePhoneResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.Created,
            statusCode: StatusCode.CreatedOne,
            data: new CreatePhoneResponse(phone.Id)
        );

        # endregion
    }
}
