using Lira.Api.Controllers.Managers.Dto;
using Lira.Api.Structs;
using Lira.Application.CQRS.Managers.Commands.CreateAdmin;
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

    [HttpPost]
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
}
