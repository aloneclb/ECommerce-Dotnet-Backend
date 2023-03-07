using ETicaret.Application.Repositories.Base;

namespace ETicaret.Application.Repositories.Product;

public interface IProductWriteRepository : IWriteRepository<Domain.Entities.Product, Guid>
{
    
}