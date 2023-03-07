namespace ETicaret.Domain.Entities.Base;

public interface IBaseEntity<T> : IEntity 
    where T : IEquatable<T>
{
    T Id { get; set; }
}

public interface IEntity
{
    
}