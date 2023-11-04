using System.Net;
using System.Transactions;
using Lira.Application.CQRS.Managers.Commands.CreateManager;
using Lira.Application.CQRS.People.Commands.CreatePerson;
using Lira.Application.Dto;
using Lira.Application.Enums;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Common.Exceptions;
using Lira.Domain.Domains.Manager;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Lira.Application.CQRS.Managers.Commands.CreateAdmin;

public class CreateAdminHandler
    : IRequestHandler<CreateAdminRequest, Response<CreateAdminResponseDto>>
{
    # region ---- properties ---------------------------------------------------

    private readonly IConfiguration _configuration;
    private readonly IMediator _mediator;
    private readonly IManagerRepository _managerRepository;

    # endregion

    # region ---- constructor --------------------------------------------------

    public CreateAdminHandler(
        IConfiguration configuration,
        IManagerRepository managerRepository,
        IMediator mediator
    )
    {
        _configuration = configuration;
        _managerRepository = managerRepository;
        _mediator = mediator;
    }

    # endregion

    public async Task<Response<CreateAdminResponseDto>> Handle(
        CreateAdminRequest request,
        CancellationToken cancellationToken
    )
    {
        using var transaction =
            new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        # region ---- code validation ------------------------------------------

        var code = _configuration["Admin:Code"]
            ?? throw new MissingEnvironmentVariableException("Admin:Code");

        if (!request.Code.Equals(code))
        {
            return new Response<CreateAdminResponseDto>(
                httpStatusCode: HttpStatusCode.BadRequest,
                statusCode: StatusCode.AdminCodeIsInvalid,
                error: new ErrorDto(
                    message: AccountsMessages.AdminCodeIsInvalid
                )
            );
        }

        # endregion

        # region ---- managers -------------------------------------------------

        var managers = await _managerRepository.FindAllAsync();

        if (managers.Any())
        {
            return new Response<CreateAdminResponseDto>(
                httpStatusCode: HttpStatusCode.UnprocessableEntity,
                statusCode: StatusCode.AdminAlreadyExists,
                error: new ErrorDto(
                    message: AccountsMessages.AdminAlreadyExists
                )
            );
        }

        # endregion

        # region ---- person ---------------------------------------------------

        var personResult = await _mediator.Send(
            new CreatePersonRequest(
                firstName: request.Name,
                surname: request.Surname,
                document: request.Cpf
            ),
            cancellationToken
        );

        if (!personResult.IsSuccess)
        {
            return new Response<CreateAdminResponseDto>(
                httpStatusCode: personResult.HttpStatusCode,
                statusCode: personResult.StatusCode,
                error: personResult.Error
            );
        }

        # endregion

        # region ---- manager --------------------------------------------------

        var managerResult = await _mediator.Send(
            new CreateManagerRequest(
                personId: personResult.Data?.Id
                          ?? throw new NullReferenceException(),
                username: request.Username,
                password: request.Password,
                confirmation: request.PasswordConfirmation
            ),
            cancellationToken
        );

        if (!managerResult.IsSuccess)
        {
            return new Response<CreateAdminResponseDto>(
                httpStatusCode: managerResult.HttpStatusCode,
                statusCode: managerResult.StatusCode,
                error: managerResult.Error
            );
        }

        var managerId = managerResult.Data?.Id
                        ?? throw new NullReferenceException();

        # endregion

        # region ---- response -------------------------------------------------

        transaction.Complete();

        return new Response<CreateAdminResponseDto>(
            httpStatusCode: HttpStatusCode.Created,
            statusCode: StatusCode.CreatedTransaction,
            isSuccess: true,
            data: new CreateAdminResponseDto(managerId)
        );

        # endregion
    }
}
