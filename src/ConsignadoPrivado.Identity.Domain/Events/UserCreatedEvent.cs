using ConsignadoPrivado.Identity.Domain.Enums;

namespace ConsignadoPrivado.Identity.Domain.Events;

public class UserCreatedEvent
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
}
