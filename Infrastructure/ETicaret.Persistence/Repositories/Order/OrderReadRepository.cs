using ETicaret.Application.Repositories.Order;
using ETicaret.Persistence.Contexts;
using ETicaret.Persistence.Repositories.Base;

namespace ETicaret.Persistence.Repositories.Order;

public class OrderReadRepository : ReadRepository<Domain.Entities.Order, Guid>, IOrderReadRepository
{
    public OrderReadRepository(ETicaretDbContext dbContext) : base(dbContext)
    {
    }
}