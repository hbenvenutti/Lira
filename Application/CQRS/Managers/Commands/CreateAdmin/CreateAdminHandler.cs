using System.Net;
using System.Transactions;
using Lira.Application.CQRS.Managers.Commands.CreateManager;
using Lira.Application.CQRS.People.Commands.CreatePerson;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Common.Enums;
using Lira.Common.Exceptions;
using Lira.Domain.Domains.Manager;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Lira.Application.CQRS.Managers.Commands.CreateAdmin;

public class CreateAdminHandler
    : IRequestHandler<CreateAdminRequest, IHandlerResponse<CreateAdminResponseDto>>
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

    public async Task<IHandlerResponse<CreateAdminResponseDto>> Handle(
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
            return new HandlerResponse<CreateAdminResponseDto>(
                httpStatusCode: HttpStatusCode.BadRequest,
                appStatusCode: AppStatusCode.InvalidAdminCode,
                errors: ManagerMessages.InvalidAdminCode
            );
        }

        # endregion

        # region ---- managers -------------------------------------------------

        var managers = await _managerRepository.FindAllAsync();

        if (managers.Any())
        {
            return new HandlerResponse<CreateAdminResponseDto>(
                httpStatusCode: HttpStatusCode.UnprocessableEntity,
                appStatusCode: AppStatusCode.AdminAlreadyExists,
                errors: ManagerMessages.AdminAlreadyExists
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
            return new HandlerResponse<CreateAdminResponseDto>(
                httpStatusCode: personResult.HttpStatusCode,
                appStatusCode: personResult.AppStatusCode,
                errors: personResult.Errors
                        ?? throw new NullReferenceException()
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
            return new HandlerResponse<CreateAdminResponseDto>(
                httpStatusCode: managerResult.HttpStatusCode,
                appStatusCode: managerResult.AppStatusCode,
                errors: managerResult.Errors
                        ?? throw new NullReferenceException()
            );
        }

        var managerId = managerResult.Data?.Id
                        ?? throw new NullReferenceException();

        # endregion

        # region ---- response -------------------------------------------------

        transaction.Complete();

        return new HandlerResponse<CreateAdminResponseDto>(
            httpStatusCode: HttpStatusCode.Created,
            appStatusCode: AppStatusCode.CreatedTransaction,
            isSuccess: true,
            data: new CreateAdminResponseDto(managerId)
        );

        # endregion
    }
}
