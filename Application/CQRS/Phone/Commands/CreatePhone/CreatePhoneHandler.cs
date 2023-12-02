using System.Net;
using Lira.Application.Responses;
using Lira.Application.Specifications.Phones;
using Lira.Common.Enums;
using Lira.Domain.Domains.Person;
using Lira.Domain.Domains.Phones;
using MediatR;

namespace Lira.Application.CQRS.Phone.Commands.CreatePhone;

public class CreatePhoneHandler
    : IRequestHandler<CreatePhoneRequest, IHandlerResponse<CreatePhoneResponse>>
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

    public async Task<IHandlerResponse<CreatePhoneResponse>> Handle(
        CreatePhoneRequest request, CancellationToken cancellationToken
    )
    {
        # region ---- specification --------------------------------------------

        var specification = new PhoneSpecification();

        var specificationData = new PhoneSpecificationDto(request.PhoneNumber);

        if (!specification.IsSatisfiedBy(specificationData))
        {
            return new HandlerResponse<CreatePhoneResponse>(
                httpStatusCode: HttpStatusCode.BadRequest,
                appStatusCode: specification.AppStatusCode,
                errors: specification.ErrorMessages
            );
        }

        # endregion

        # region ---- person validation ----------------------------------------

        if (!request.ValidatePerson) { goto phone; }

        var person = await _personRepository.FindByIdAsync(request.PersonId);

        if (person is null)
        {
            return new HandlerResponse<CreatePhoneResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                appStatusCode: AppStatusCode.PersonNotFound,
                errors: PersonMessages.NotFound
            );
        }

        # endregion

        # region ---- phone ----------------------------------------------------

        phone:

        var phone = await _phoneRepository.CreateAsync(
            PhoneDomain.Create(
                phone: request.PhoneNumber,
                personId: request.PersonId
            )
        );

        # endregion

        # region ---- response -------------------------------------------------

        return new HandlerResponse<CreatePhoneResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.Created,
            appStatusCode: AppStatusCode.CreatedOne,
            data: new CreatePhoneResponse(phone.Id)
        );

        # endregion
    }
}
