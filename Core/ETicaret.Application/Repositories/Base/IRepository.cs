using ETicaret.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Application.Repositories.Base;

public interface IRepository<TEntity, in TKey>
    where TEntity : class, IBaseEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    DbSet<TEntity> Table { get; }
}