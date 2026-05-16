using ConsignadoPrivado.Common;
using ConsignadoPrivado.Identity.Application.AuthenticateUser;
using ConsignadoPrivado.Identity.WebApi.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ConsignadoPrivado.Identity.WebApi.Features.Auth;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponseWithData<AuthenticateUserResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return OkResponse(result);
    }
}
