using System.Net;
using Lira.Application.Dto;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Application.Specifications.Address;
using Lira.Domain.Domains.Address;
using Lira.Domain.Domains.Person;
using MediatR;

namespace Lira.Application.CQRS.Address.Commands.CreateAddress;

public class CreateAddressHandler
    : IRequestHandler<CreateAddressRequest, Response<CreateAddressResponse>>
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

    public async Task<Response<CreateAddressResponse>> Handle(
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
            return new Response<CreateAddressResponse>(
                httpStatusCode: HttpStatusCode.BadRequest,
                statusCode: addressSpecification.StatusCode,
                error: new ErrorDto(addressSpecification.ErrorMessages)
            );
        }

        # endregion

        # region ---- person ---------------------------------------------------

        if (!request.ValidatePerson) { goto address; }

        var person = await _personRepository.FindByIdAsync(request.PersonId);

        if (person is null)
        {
            return new Response<CreateAddressResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                statusCode: StatusCode.PersonNotFound,
                error: new ErrorDto(message: NotFoundMessages.PersonNotFound)
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

        return new Response<CreateAddressResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.Created,
            statusCode: StatusCode.CreatedOne,
            data: new CreateAddressResponse(address.Id)
        );

        # endregion
    }
}
