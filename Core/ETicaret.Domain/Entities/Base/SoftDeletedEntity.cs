namespace ETicaret.Domain.Entities.Base;

public class SoftDeletedEntity<T> : BaseEntity<T>, ISoftDelete where T : IEquatable<T>
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime IsDeletedAt { get; set; }
}