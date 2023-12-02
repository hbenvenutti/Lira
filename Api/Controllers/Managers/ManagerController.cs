using System.Net;
using Lira.Api.Controllers.Managers.Dto;
using Lira.Api.Structs;
using Lira.Application.CQRS.Accounts.Commands.Login;
using Lira.Application.CQRS.Managers.Commands.CreateAdmin;
using Lira.Application.Responses;
using Lira.Common.Structs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lira.Api.Controllers.Managers;

[ApiController]
[Route(template: $"/{ApiVersions.Version1}/managers")]
[Produces(contentType: HttpContentTypes.Json)]

public class ManagerController : ControllerBase
{
    private readonly IMediator _mediator;

    public ManagerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(template: "admin")]
    [ProducesResponseType(
        typeof(IHandlerResponse<CreateAdminResponseDto>),
        statusCode: (int) HttpStatusCode.Created)
    ]
    [ProducesResponseType(
        typeof(IHandlerResponse<CreateAdminResponseDto>),
        statusCode: (int) HttpStatusCode.BadRequest)
    ]
    [ProducesResponseType(
        typeof(IHandlerResponse<CreateAdminResponseDto>),
        statusCode: (int) HttpStatusCode.UnprocessableEntity)
    ]
    [ProducesResponseType(
        typeof(IHandlerResponse<CreateAdminResponseDto>),
        statusCode: (int) HttpStatusCode.InternalServerError)
    ]

    public async Task<IActionResult> CreateAdminAsync(
        [FromBody] CreateAdminBodyDto body
    )
    {
        var response = await _mediator.Send(request: (CreateAdminRequest) body);

        return StatusCode(
            statusCode: (int) response.HttpStatusCode,
            value: response
        );
    }

    [HttpPost(template: "signin")]
    [ProducesResponseType(
        typeof(IHandlerResponse<SignInResponse>),
        statusCode: (int) HttpStatusCode.OK)
    ]
    [ProducesResponseType(
        typeof(IHandlerResponse<SignInResponse>),
        statusCode: (int) HttpStatusCode.BadRequest)
    ]
    [ProducesResponseType(
        typeof(IHandlerResponse<SignInResponse>),
        statusCode: (int) HttpStatusCode.NotFound)
    ]
    [ProducesResponseType(
        typeof(IHandlerResponse<SignInResponse>),
        statusCode: (int) HttpStatusCode.InternalServerError)
    ]
    public async Task<IActionResult> SignInAsync(
        [FromBody] SignInBodyDto body
    )
    {
        var response = await _mediator.Send(request: (SignInRequest) body);

        return StatusCode(
            statusCode: (int) response.HttpStatusCode,
            value: response
        );
    }
}
