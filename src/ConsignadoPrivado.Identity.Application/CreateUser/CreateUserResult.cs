using ConsignadoPrivado.Identity.Domain.Enums;

namespace ConsignadoPrivado.Identity.Application.CreateUser;

public class CreateUserResult
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CPF { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
