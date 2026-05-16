using ConsignadoPrivado.Common;
using ConsignadoPrivado.Identity.Application.CreateUser;
using ConsignadoPrivado.Identity.Application.GetUser;
using ConsignadoPrivado.Identity.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConsignadoPrivado.Identity.WebApi.Features.Users;

[ApiController]
[Route("api/[controller]")]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateUserResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return CreatedResponse(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetUserResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new GetUserCommand { Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return OkResponse(result);
    }
}
