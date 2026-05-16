namespace ConsignadoPrivado.Identity.Domain.Events;

public class UserBlockedEvent
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public DateTime BlockedAt { get; set; }
}
