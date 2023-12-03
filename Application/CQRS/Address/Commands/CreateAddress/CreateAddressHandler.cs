using System.Net;
using Lira.Application.CQRS.People.Queries.GetPersonById;
using Lira.Application.Responses;
using Lira.Common.Enums;
using Lira.Domain.Domains.Address;
using MediatR;

namespace Lira.Application.CQRS.Address.Commands.CreateAddress;

public class CreateAddressHandler
    : IRequestHandler<CreateAddressRequest, IHandlerResponse<CreateAddressResponse>>
{
    # region ---- properties ---------------------------------------------------

    private readonly IMediator _mediator;
    private readonly IAddressRepository _addressRepository;

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateAddressHandler(
        IMediator mediator,
        IAddressRepository addressRepository
    )
    {
        _mediator = mediator;
        _addressRepository = addressRepository;
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

        if (request.ValidatePerson)
        {
            var personRequest = new GetPersonByIdRequest(request.PersonId);

            var personResult = await _mediator.Send(
                personRequest,
                cancellationToken
            );

            if (!personResult.IsSuccess)
            {
                return new HandlerResponse<CreateAddressResponse>(
                    httpStatusCode: personResult.HttpStatusCode,
                    appStatusCode: personResult.AppStatusCode,
                    errors: personResult.Errors ?? new List<string>()
                );
            }
        }

        # endregion

        # region ---- address --------------------------------------------------

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
