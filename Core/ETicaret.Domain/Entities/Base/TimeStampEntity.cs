namespace ETicaret.Domain.Entities.Base;

public class TimeStampEntity<T> : BaseEntity<T>, ITimeStamp where T : IEquatable<T>
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}