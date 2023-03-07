using ETicaret.Application.Repositories.Base;

namespace ETicaret.Application.Repositories.Order;

public interface IOrderReadRepository : IReadRepository<Domain.Entities.Order, Guid>
{
    
}