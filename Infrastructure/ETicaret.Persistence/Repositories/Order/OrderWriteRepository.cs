using ETicaret.Application.Repositories.Order;
using ETicaret.Persistence.Contexts;
using ETicaret.Persistence.Repositories.Base;

namespace ETicaret.Persistence.Repositories.Order;

public class OrderWriteRepository : WriteRepository<Domain.Entities.Order, Guid>, IOrderWriteRepository
{
    public OrderWriteRepository(ETicaretDbContext dbContext) : base(dbContext)
    {
    }
}