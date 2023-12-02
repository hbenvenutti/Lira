using System.Net;
using Lira.Application.Messages;
using Lira.Application.Responses;
using Lira.Application.Services.Token;
using Lira.Common.Enums;
using Lira.Common.Types;
using Lira.Domain.Domains.Manager;
using MediatR;

namespace Lira.Application.CQRS.Accounts.Commands.Login;

public class SignInHandler
    : IRequestHandler<SignInRequest, IHandlerResponse<SignInResponse>>
{
    private readonly IManagerRepository _managerRepository;
    private readonly ITokenService _tokenService;

    public SignInHandler(
        IManagerRepository managerRepository,
        ITokenService tokenService
    )
    {
        _managerRepository = managerRepository;
        _tokenService = tokenService;
    }

    public async Task<IHandlerResponse<SignInResponse>> Handle(
        SignInRequest request,
        CancellationToken cancellationToken
    )
    {
        var isPasswordValid = Password
            .TryParse(request.Password, out var password);

        var isUsernameValid = Username
            .TryParse(request.Username, out var username);

        if (!isPasswordValid || !isUsernameValid)
        {
            return new HandlerResponse<SignInResponse>(
                httpStatusCode: HttpStatusCode.BadRequest,
                appStatusCode: AppStatusCode.SignInFailed,
                errors: ManagerMessages.InvalidUsernameOrPassword
            );
        }

        var manager = await _managerRepository
            .FindByUsernameAsync(username);

        if (manager is null)
        {
            return new HandlerResponse<SignInResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                appStatusCode: AppStatusCode.SignInFailed,
                errors: ManagerMessages.InvalidUsernameOrPassword
            );
        }

        if (!Password.Compare(manager.Password, password))
        {
            return new HandlerResponse<SignInResponse>(
                httpStatusCode: HttpStatusCode.NotFound,
                appStatusCode: AppStatusCode.SignInFailed,
                errors: ManagerMessages.InvalidUsernameOrPassword
            );
        }

        var token = _tokenService.Sign(manager);

        return new HandlerResponse<SignInResponse>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.OK,
            appStatusCode: AppStatusCode.SignInSuccess,
            data: new SignInResponse(token)
        );
    }
}
