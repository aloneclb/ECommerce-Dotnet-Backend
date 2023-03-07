using ETicaret.Application.Repositories.Base;

namespace ETicaret.Application.Repositories.Product;

public interface IProductReadRepository : IReadRepository<Domain.Entities.Product, Guid>
{
    
}