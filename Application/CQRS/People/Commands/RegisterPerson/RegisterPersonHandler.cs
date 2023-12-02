using System.Net;
using System.Transactions;
using Lira.Application.CQRS.Address.Commands.CreateAddress;
using Lira.Application.CQRS.Emails.Commands.CreateEmail;
using Lira.Application.CQRS.Medium.Commands.CreateMedium;
using Lira.Application.CQRS.People.Commands.CreatePerson;
using Lira.Application.CQRS.PersonOrixa.Commands.CreatePersonOrixa;
using Lira.Application.CQRS.Phone.Commands.CreatePhone;
using Lira.Application.Enums;
using Lira.Application.Responses;
using Lira.Domain.Enums;
using Lira.Domain.Religion.Enums;
using MediatR;

namespace Lira.Application.CQRS.People.Commands.RegisterPerson;

public class RegisterPersonHandler
    : IRequestHandler<RegisterPersonRequest, HandlerResponse<RegisterPersonResponse>>
{
    # region ---- properties ---------------------------------------------------

    private readonly IMediator _mediator;

    # endregion

    # region ---- constructor --------------------------------------------------

    public RegisterPersonHandler(
        IMediator mediator
    )
    {
        _mediator = mediator;
    }

    # endregion

    public async Task<HandlerResponse<RegisterPersonResponse>> Handle(
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
            return new HandlerResponse<RegisterPersonResponse>(
                httpStatusCode: personResult.HttpStatusCode,
                appStatusCode: personResult.AppStatusCode,
                errors: personResult.Errors
                        ?? throw new NullReferenceException()
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
            return new HandlerResponse<RegisterPersonResponse>(
                httpStatusCode: addressResult.HttpStatusCode,
                appStatusCode: addressResult.AppStatusCode,
                errors: addressResult.Errors
                        ?? throw new NullReferenceException()
            );
        }

        # endregion

        # region ---- email ----------------------------------------------------

        var emailResult = await _mediator.Send(
            new CreateEmailRequest(
                personId: personId,
                address: request.Email,
                type: request.EmailType ?? EmailType.Personal,
                validatePerson: false
            ),
            cancellationToken
        );

        if (!emailResult.IsSuccess)
        {
            return new HandlerResponse<RegisterPersonResponse>(
                httpStatusCode: emailResult.HttpStatusCode,
                appStatusCode: emailResult.AppStatusCode,
                errors: emailResult.Errors
                        ?? throw new NullReferenceException()
            );
        }

        # endregion

        # region ---- phone ----------------------------------------------------

        var phoneResult = await _mediator.Send(
            new CreatePhoneRequest(
                personId: personId,
                phoneNumber: request.PhoneNumber,
                validatePerson: false
            ),
            cancellationToken
        );

        if (!phoneResult.IsSuccess)
        {
            return new HandlerResponse<RegisterPersonResponse>(
                httpStatusCode: phoneResult.HttpStatusCode,
                appStatusCode: phoneResult.AppStatusCode,
                errors: phoneResult.Errors
                        ?? throw new NullReferenceException()
            );
        }

        # endregion

        # region ---- medium ---------------------------------------------------

        if (request.IsMedium)
        {
            var mediumResult = await _mediator.Send(
                request: new CreateMediumRequest(
                    personId: personId,
                    firstAmaci: request.FirstAmaci,
                    lastAmaci: request.LastAmaci,
                    validatePerson: false
                ),
                cancellationToken: cancellationToken
            );

            if (!mediumResult.IsSuccess)
            {
                return new HandlerResponse<RegisterPersonResponse>(
                    httpStatusCode: mediumResult.HttpStatusCode,
                    appStatusCode: mediumResult.AppStatusCode,
                    errors: mediumResult.Errors
                            ?? throw new NullReferenceException()
                );
            }
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
                return new HandlerResponse<RegisterPersonResponse>(
                    httpStatusCode: adjunctOrixaResult.HttpStatusCode,
                    appStatusCode: adjunctOrixaResult.AppStatusCode,
                    errors: adjunctOrixaResult.Errors
                            ?? throw new NullReferenceException()
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
                return new HandlerResponse<RegisterPersonResponse>(
                    httpStatusCode: ancestralOrixaResult.HttpStatusCode,
                    appStatusCode: ancestralOrixaResult.AppStatusCode,
                    errors: ancestralOrixaResult.Errors
                            ?? throw new NullReferenceException()
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
                return new HandlerResponse<RegisterPersonResponse>(
                    httpStatusCode: frontOrixaResult.HttpStatusCode,
                    appStatusCode: frontOrixaResult.AppStatusCode,
                    errors: frontOrixaResult.Errors
                            ?? throw new NullReferenceException()
                );
            }
        }

        # endregion

        # region ---- success --------------------------------------------------

        transaction.Complete();

        return new HandlerResponse<RegisterPersonResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.Created,
            appStatusCode: AppStatusCode.CreatedTransaction,
            data: new RegisterPersonResponse(id: personId)
        );

        # endregion
    }
}
