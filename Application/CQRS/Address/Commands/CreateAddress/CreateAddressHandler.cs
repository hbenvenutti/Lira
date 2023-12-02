using System.Net;
using Lira.Application.Responses;
using Lira.Common.Enums;
using Lira.Domain.Domains.Address;
using Lira.Domain.Domains.Person;
using MediatR;

namespace Lira.Application.CQRS.Address.Commands.CreateAddress;

public class CreateAddressHandler
    : IRequestHandler<CreateAddressRequest, IHandlerResponse<CreateAddressResponse>>
{
    # region ---- properties ---------------------------------------------------

    private readonly IAddressRepository _addressRepository;
    private readonly IPersonRepository _personRepository;

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateAddressHandler(
        IAddressRepository addressRepository,
        IPersonRepository personRepository
    )
    {
        _addressRepository = addressRepository;
        _personRepository = personRepository;
    }

    # endregion

    public async Task<IHandlerResponse<CreateAddressResponse>> Handle(
        CreateAddressRequest request,
        CancellationToken cancellationToken
    )
    {
        # region ---- specifications -------------------------------------------

        var addressSpecification = new AddressSpecification();

        var addressData = new AddressSpecificationDto(
            street: request.Street,
            number: request.Number,
            neighborhood: request.Neighborhood,
            city: request.City,
            state: request.State,
            zipCode: request.ZipCode
        );

        if (!addressSpecification.IsSatisfiedBy(addressData))
        {
            return new HandlerResponse<CreateAddressResponse>(
                httpStatusCode: HttpStatusCode.BadRequest,
                appStatusCode: addressSpecification.AppStatusCode,
                errors: addressSpecification.ErrorMessages
            );
        }

        # endregion

        # region ---- person ---------------------------------------------------

        if (!request.ValidatePerson) { goto address; }

        var person = await _personRepository.FindByIdAsync(request.PersonId);

        if (person is null)
        {
            return new HandlerResponse<CreateAddressResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                appStatusCode: AppStatusCode.PersonNotFound,
                errors: PersonMessages.NotFound
            );
        }

        # endregion

        # region ---- address --------------------------------------------------

        address:

        var address = AddressDomain.Create(
            street: request.Street,
            number: request.Number,
            neighborhood: request.Neighborhood,
            city: request.City,
            state: request.State,
            zipCode: request.ZipCode,
            personId: request.PersonId,
            complement: request.Complement
        );

        address = await _addressRepository.CreateAsync(address);

        # endregion

        # region ---- response -------------------------------------------------

        return new HandlerResponse<CreateAddressResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.Created,
            appStatusCode: AppStatusCode.CreatedOne,
            data: new CreateAddressResponse(address.Id)
        );

        # endregion
    }
}
