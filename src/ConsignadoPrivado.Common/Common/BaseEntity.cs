namespace ConsignadoPrivado.Common;

public abstract class BaseEntity : IComparable<BaseEntity>
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }

    public int CompareTo(BaseEntity? other)
    {
        if (other == null) return 1;
        return other.Id.CompareTo(Id);
    }
}
