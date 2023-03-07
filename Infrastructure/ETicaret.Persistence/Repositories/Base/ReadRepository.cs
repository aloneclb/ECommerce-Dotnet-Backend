using System.Linq.Expressions;
using ETicaret.Application.Repositories.Base;
using ETicaret.Domain.Entities.Base;
using ETicaret.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETicaret.Persistence.Repositories.Base;

public class ReadRepository<TEntity, TKey> : IReadRepository<TEntity, TKey> 
    where TEntity : class, IBaseEntity<TKey>, new()
    where TKey : IEquatable<TKey>
{
    private readonly ETicaretDbContext _dbContext;
    
    public ReadRepository(ETicaretDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbSet<TEntity> Table => _dbContext.Set<TEntity>();
    
    public IQueryable<TEntity> AsQueryable()
    {
        return Table.AsNoTracking();
    }

    public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> expression)
    {
        return AsQueryable().Where(expression);
    }

    public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await AsQueryable().FirstOrDefaultAsync(expression);
    }

    public async Task<TEntity?> GetByIdAsync(TKey id)
    {
        return await AsQueryable().FirstOrDefaultAsync(x => x.Id.Equals(id));
        // return await AsQueryable().FindAsync(x => x.Id.Equals(id));
    }
}