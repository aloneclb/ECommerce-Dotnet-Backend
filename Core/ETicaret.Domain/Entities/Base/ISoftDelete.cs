namespace ETicaret.Domain.Entities.Base;

public interface ISoftDelete : ITimeStamp
{
    public bool IsDeleted { get; set; }
    public DateTime IsDeletedAt { get; set; }
}