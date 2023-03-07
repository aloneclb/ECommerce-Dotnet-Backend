using ETicaret.Application.Repositories.Base;

namespace ETicaret.Application.Repositories.Order;

public interface IOrderWriteRepository : IWriteRepository<Domain.Entities.Order, Guid>
{
    
}