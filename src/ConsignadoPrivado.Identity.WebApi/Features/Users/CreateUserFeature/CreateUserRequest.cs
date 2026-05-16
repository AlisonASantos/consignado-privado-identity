using ConsignadoPrivado.Identity.Domain.Enums;

namespace ConsignadoPrivado.Identity.WebApi.Features.Users.CreateUserFeature;

public class CreateUserRequest
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}
