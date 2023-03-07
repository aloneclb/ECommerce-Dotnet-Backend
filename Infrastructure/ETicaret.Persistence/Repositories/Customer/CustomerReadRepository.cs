using ETicaret.Application.Repositories.Customer;
using ETicaret.Persistence.Contexts;
using ETicaret.Persistence.Repositories.Base;

namespace ETicaret.Persistence.Repositories.Customer;

public class CustomerReadRepository : ReadRepository<Domain.Entities.Customer, Guid>, ICustomerReadRepository
{
    public CustomerReadRepository(ETicaretDbContext dbContext) : base(dbContext)
    {
    }
}