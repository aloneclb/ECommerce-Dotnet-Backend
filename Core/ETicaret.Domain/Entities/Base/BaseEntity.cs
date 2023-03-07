namespace ETicaret.Domain.Entities.Base;

public class BaseEntity<T> : IBaseEntity<T> where T : IEquatable<T>
{
    public T Id { get; set; } = default!;
}