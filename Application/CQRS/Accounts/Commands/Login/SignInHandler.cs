using System.Net;
using Lira.Application.CQRS.Accounts.Commands.Login.Dto;
using Lira.Application.Dto;
using Lira.Application.Enums;
using Lira.Application.Errors;
using Lira.Application.Responses;
using Lira.Application.Services.Token;
using Lira.Common.Types;
using Lira.Domain.Domains.Manager;
using MediatR;

namespace Lira.Application.CQRS.Accounts.Commands.Login;

public class SignInHandler
    : IRequestHandler<SignInRequest, Response<SignInResponseDto>>
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

    public async Task<Response<SignInResponseDto>> Handle(
        SignInRequest request,
        CancellationToken cancellationToken
    )
    {
        if (!Username.TryParse(request.Username, out var username))
        {
            return new Response<SignInResponseDto>(
                httpStatusCode: HttpStatusCode.BadRequest,
                statusCode: StatusCode.SignInFailed,
                error: new ErrorDto(message: AccountsMessages
                    .InvalidUsernameOrPassword
                )
            );
        }

        if (!Password.TryParse(request.Password, out var password))
        {
            return new Response<SignInResponseDto>(
                httpStatusCode: HttpStatusCode.BadRequest,
                statusCode: StatusCode.SignInFailed,
                error: new ErrorDto(message: AccountsMessages
                    .InvalidUsernameOrPassword
                )
            );
        }

        var manager = await _managerRepository
            .FindByUsernameAsync(username);

        if (manager is null)
        {
            return new Response<SignInResponseDto>(
                httpStatusCode: HttpStatusCode.NotFound,
                statusCode: StatusCode.SignInFailed,
                error: new ErrorDto(message: AccountsMessages
                    .InvalidUsernameOrPassword
                )
            );
        }

        if (!Password.Compare(manager.Password, password))
        {
            return new Response<SignInResponseDto>(
                httpStatusCode: HttpStatusCode.NotFound,
                statusCode: StatusCode.SignInFailed,
                error: new ErrorDto(message: AccountsMessages
                    .InvalidUsernameOrPassword
                )
            );
        }

        var token = _tokenService.Sign(manager);

        return new Response<SignInResponseDto>(
            isSuccess: true,
            httpStatusCode: HttpStatusCode.OK,
            statusCode: StatusCode.SignInSuccess,
            data: new SignInResponseDto(token)
        );
    }
}
