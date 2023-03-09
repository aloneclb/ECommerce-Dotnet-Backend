using ETicaret.Application.Repositories.ProductImages;
using ETicaret.Persistence.Contexts;
using ETicaret.Persistence.Repositories.Base;

namespace ETicaret.Persistence.Repositories.ProductImage;

public class ProductImageWriteRepository : WriteRepository<Domain.Entities.ProductImage, int>,
    IProductImageWriteRepository
{
    public ProductImageWriteRepository(ETicaretDbContext dbContext) : base(dbContext)
    {
    }
}