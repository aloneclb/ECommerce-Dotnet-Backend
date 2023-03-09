using ETicaret.Application.Repositories.Base;

namespace ETicaret.Application.Repositories.ProductImages;

public interface IProductImageWriteRepository : IWriteRepository<Domain.Entities.ProductImage, int>
{
}