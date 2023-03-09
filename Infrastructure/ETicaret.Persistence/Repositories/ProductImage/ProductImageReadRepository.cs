using ETicaret.Application.Repositories.ProductImages;
using ETicaret.Persistence.Contexts;
using ETicaret.Persistence.Repositories.Base;

namespace ETicaret.Persistence.Repositories.ProductImage;

public class ProductImageReadRepository : ReadRepository<Domain.Entities.ProductImage, int>,
    IProductImageReadRepository
{
    public ProductImageReadRepository(ETicaretDbContext dbContext) : base(dbContext)
    {
    }
}