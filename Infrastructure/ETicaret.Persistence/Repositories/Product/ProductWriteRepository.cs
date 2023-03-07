using ETicaret.Application.Repositories.Product;
using ETicaret.Persistence.Contexts;
using ETicaret.Persistence.Repositories.Base;

namespace ETicaret.Persistence.Repositories.Product;

public class ProductWriteRepository : WriteRepository<Domain.Entities.Product, Guid>, IProductWriteRepository
{
    public ProductWriteRepository(ETicaretDbContext dbContext) : base(dbContext)
    {
    }
}