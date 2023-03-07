using ETicaret.Application.Repositories.Base;
using ETicaret.Domain.Entities.Base;
using ETicaret.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Repositories.Base;

public class WriteRepository<TEntity, TKey> : IWriteRepository<TEntity, TKey>
    where TEntity : class, IBaseEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    private readonly ETicaretDbContext _dbContext;

    public WriteRepository(ETicaretDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbSet<TEntity> Table => _dbContext.Set<TEntity>();

    public async Task<bool> AddAsync(TEntity model)
    {
        _dbContext.Attach(model);
        await Table.AddAsync(model);

        return true;
    }

    public async Task<bool> BulkAddAsync(List<TEntity> models)
    {
        _dbContext.AttachRange(models);
        await Table.AddRangeAsync(models);
        return true;
    }

    public bool Remove(TEntity model)
    {
        _dbContext.Attach(model);
        Table.Remove(model);
        return true;
    }

    public bool BulkRemove(List<TEntity> models)
    {
        _dbContext.AttachRange(models);
        Table.RemoveRange(models);
        return true;
    }

    public bool Update(TEntity model)
    {
        _dbContext.Attach(model);
        Table.Update(model);
        return true;
    }

    public async Task<int> SaveChanges()
        => await _dbContext.SaveChangesAsync();
}