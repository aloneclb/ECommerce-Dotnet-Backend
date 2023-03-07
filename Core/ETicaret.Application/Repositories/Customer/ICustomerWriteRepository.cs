using ETicaret.Application.Repositories.Base;

namespace ETicaret.Application.Repositories.Customer;

public interface ICustomerWriteRepository : IWriteRepository<Domain.Entities.Customer, Guid>
{
    
}