using System.Linq.Expressions;
using ETicaret.Domain.Entities.Base;

namespace ETicaret.Application.Repositories.Base;

public interface IReadRepository<TEntity, in TKey> : IRepository<TEntity, TKey> 
    where TEntity : class, IBaseEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    IQueryable<TEntity> AsQueryable();
    IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression);
    Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity?> GetByIdAsync(TKey id);
}