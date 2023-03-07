namespace ETicaret.Domain.Entities.Base;

public interface ITimeStamp
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}