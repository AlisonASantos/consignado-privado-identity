using ConsignadoPrivado.Identity.Domain.Enums;
using MediatR;

namespace ConsignadoPrivado.Identity.Application.CreateUser;

public class CreateUserCommand : IRequest<CreateUserResult>
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}
