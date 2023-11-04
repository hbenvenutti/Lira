using Lira.Api.Controllers.People.Dto;
using Lira.Api.Structs;
using Lira.Application.CQRS.People.Commands.RegisterPerson;
using Lira.Common.Structs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lira.Api.Controllers.People;

[ApiController]
[Route(template: $"/{ApiVersions.Version1}/people")]
[Produces(contentType: HttpContentTypes.Json)]
public class PeopleController : ControllerBase
{
    private readonly IMediator _mediator;

    public PeopleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize]
    [HttpPost(template: "register")]
    public async Task<IActionResult> RegisterPersonAsync(
        [FromBody] RegisterPersonBodyDto body
    )
    {
        var response = await _mediator
            .Send(request: (RegisterPersonRequest) body);

        return StatusCode(
            statusCode: (int) response.HttpStatusCode,
            value: response
        );
    }
}
