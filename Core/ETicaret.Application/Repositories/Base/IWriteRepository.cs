using ETicaret.Domain.Entities.Base;

namespace ETicaret.Application.Repositories.Base;

public interface IWriteRepository<TEntity, in TKey> : IRepository<TEntity, TKey>
    where TEntity : class, IBaseEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    Task<bool> AddAsync(TEntity model);
    Task<bool> BulkAddAsync(List<TEntity> models);
    bool Remove(TEntity model);
    bool BulkRemove(List<TEntity> models);
    bool Update(TEntity model);
    Task<int> SaveChanges();
}