using MediatR;

namespace ConsignadoPrivado.Identity.Application.AuthenticateUser;

public class AuthenticateUserCommand : IRequest<AuthenticateUserResult>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
