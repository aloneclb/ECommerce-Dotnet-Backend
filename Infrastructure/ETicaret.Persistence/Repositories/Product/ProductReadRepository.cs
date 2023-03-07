using ETicaret.Application.Repositories.Product;
using ETicaret.Persistence.Contexts;
using ETicaret.Persistence.Repositories.Base;

namespace ETicaret.Persistence.Repositories.Product;

public class ProductReadRepository : ReadRepository<Domain.Entities.Product, Guid>, IProductReadRepository
{
    public ProductReadRepository(ETicaretDbContext dbContext) : base(dbContext)
    {
    }
}