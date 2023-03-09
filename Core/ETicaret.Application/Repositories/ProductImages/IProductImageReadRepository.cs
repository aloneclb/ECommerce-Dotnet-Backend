using ETicaret.Application.Repositories.Base;

namespace ETicaret.Application.Repositories.ProductImages;

public interface IProductImageReadRepository : IReadRepository<Domain.Entities.ProductImage, int>
{
}