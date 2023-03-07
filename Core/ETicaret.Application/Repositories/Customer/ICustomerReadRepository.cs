using ETicaret.Application.Repositories.Base;

namespace ETicaret.Application.Repositories.Customer;

public interface ICustomerReadRepository : IReadRepository<Domain.Entities.Customer, Guid>
{
    
}