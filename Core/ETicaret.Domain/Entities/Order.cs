using ETicaret.Domain.Entities.Base;

namespace ETicaret.Domain.Entities;

public class Order : BaseEntity<Guid>
{
    public string Description { get; set; }
    public string Address { get; set; }
    
    // Foreign Key's
    public Guid CustomerId { get; set; }
    
    // Navigation Props
    public Customer Customer { get; set; }
    public ICollection<Product> Products { get; set; }
}