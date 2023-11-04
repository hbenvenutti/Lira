using System.Net;
using System.Transactions;
using Lira.Application.CQRS.Address.Commands.CreateAddress;
using Lira.Application.CQRS.People.Commands.CreatePerson;
using Lira.Application.CQRS.PersonOrixa.Commands.CreatePersonOrixa;
using Lira.Application.CQRS.Phone.Commands.CreatePhone;
using Lira.Application.Enums;
using Lira.Application.Responses;
using Lira.Domain.Domains.Medium;
using Lira.Domain.Religion.Enums;
using MediatR;

namespace Lira.Application.CQRS.People.Commands.RegisterPerson;

public class RegisterPersonHandler
    : IRequestHandler<RegisterPersonRequest, Response<RegisterPersonResponse>>
{
    # region ---- properties ---------------------------------------------------

    private readonly IMediator _mediator;
    private readonly IMediumRepository _mediumRepository;

    # endregion

    # region ---- constructor --------------------------------------------------

    public RegisterPersonHandler(
        IMediumRepository mediumRepository,
        IMediator mediator
    )
    {
        _mediumRepository = mediumRepository;
        _mediator = mediator;
    }

    # endregion

    public async Task<Response<RegisterPersonResponse>> Handle(
        RegisterPersonRequest request,
        CancellationToken cancellationToken
    )
    {
        using var transaction =
            new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        # region ---- person ---------------------------------------------------

        var personResult = await _mediator.Send(
            new CreatePersonRequest(
                firstName: request.FirstName,
                surname: request.Surname,
                document: request.Document
            ),
            cancellationToken
        );

        if (!personResult.IsSuccess)
        {
            return new Response<RegisterPersonResponse>(
                httpStatusCode: personResult.HttpStatusCode,
                statusCode: personResult.StatusCode,
                error: personResult.Error
            );
        }

        var personId = personResult.Data?.Id
                       ?? throw new NullReferenceException();

        # endregion

        # region ---- address --------------------------------------------------

        var addressResult = await _mediator.Send(
            new CreateAddressRequest(
                street: request.Street,
                number: request.Number,
                neighborhood: request.Neighborhood,
                city: request.City,
                state: request.State,
                zipCode: request.ZipCode,
                personId: personId,
                complement: request.Complement,
                validatePerson: false
            ),
            cancellationToken
        );

        if (!addressResult.IsSuccess)
        {
            return new Response<RegisterPersonResponse>(
                httpStatusCode: addressResult.HttpStatusCode,
                statusCode: addressResult.StatusCode,
                error: addressResult.Error
            );
        }

        # endregion

        # region ---- phone ----------------------------------------------------

        var phoneResult = await _mediator.Send(
            new CreatePhoneRequest(
                personId: personId,
                phoneNumber: request.PhoneNumber
            ),
            cancellationToken
        );

        if (!phoneResult.IsSuccess)
        {
            return new Response<RegisterPersonResponse>(
                httpStatusCode: phoneResult.HttpStatusCode,
                statusCode: phoneResult.StatusCode,
                error: phoneResult.Error
            );
        }

        # endregion

        # region ---- medium ---------------------------------------------------

        if (request.IsMedium)
        {
            await _mediumRepository.CreateAsync(MediumDomain.Create(
                personId: personId,
                request.FirstAmaci,
                request.LastAmaci
            ));
        }

        # endregion

        # region ---- orixas ---------------------------------------------------

        if (request.AdjunctOrixaId is not null)
        {
            var adjunctOrixaResult = await _mediator.Send(
                new CreatePersonOrixaRequest(
                    personId: personId,
                    orixaId: (Guid) request.AdjunctOrixaId,
                    type: OrixaType.Adjunct,
                    validatePerson: false
                ),
                cancellationToken: cancellationToken
            );

            if (!adjunctOrixaResult.IsSuccess)
            {
                return new Response<RegisterPersonResponse>(
                    httpStatusCode: adjunctOrixaResult.HttpStatusCode,
                    statusCode: adjunctOrixaResult.StatusCode,
                    error: adjunctOrixaResult.Error
                );
            }
        }

        if (request.AncestralOrixaId is not null)
        {
            var ancestralOrixaResult = await _mediator.Send(
                new CreatePersonOrixaRequest(
                    personId: personId,
                    orixaId: (Guid) request.AncestralOrixaId,
                    type: OrixaType.Ancestral,
                    validatePerson: false
                ),
                cancellationToken: cancellationToken
            );

            if (!ancestralOrixaResult.IsSuccess)
            {
                return new Response<RegisterPersonResponse>(
                    httpStatusCode: ancestralOrixaResult.HttpStatusCode,
                    statusCode: ancestralOrixaResult.StatusCode,
                    error: ancestralOrixaResult.Error
                );
            }
        }

        if (request.FrontOrixaId is not null)
        {
            var frontOrixaResult = await _mediator.Send(
                new CreatePersonOrixaRequest(
                    personId: personId,
                    orixaId: (Guid) request.FrontOrixaId,
                    type: OrixaType.Front,
                    validatePerson: false
                ),
                cancellationToken: cancellationToken
            );

            if (!frontOrixaResult.IsSuccess)
            {
                return new Response<RegisterPersonResponse>(
                    httpStatusCode: frontOrixaResult.HttpStatusCode,
                    statusCode: frontOrixaResult.StatusCode,
                    error: frontOrixaResult.Error
                );
            }
        }

        # endregion

        // todo data validation
        // todo check email already exists
        // todo create email
        // todo create a handler for email
        // todo create a handler for medium

        # region ---- success --------------------------------------------------

        transaction.Complete();

        return new Response<RegisterPersonResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.Created,
            statusCode: StatusCode.CreatedTransaction,
            data: new RegisterPersonResponse(id: personId)
        );

        # endregion
    }
}
