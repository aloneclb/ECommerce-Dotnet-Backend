using ETicaret.Application.Repositories.Customer;
using ETicaret.Persistence.Contexts;
using ETicaret.Persistence.Repositories.Base;

namespace ETicaret.Persistence.Repositories.Customer;

public class CustomerWriteRepository : WriteRepository<Domain.Entities.Customer, Guid>, ICustomerWriteRepository
{
    public CustomerWriteRepository(ETicaretDbContext dbContext) : base(dbContext)
    {
    }
}