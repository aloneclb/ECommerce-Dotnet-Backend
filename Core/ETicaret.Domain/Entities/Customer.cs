using ETicaret.Domain.Entities.Base;

namespace ETicaret.Domain.Entities;

public class Customer : SoftDeletedEntity<Guid>
{
    public string Name { get; set; }
    public int Age { get; set; }
    
    // Navigation Props
    public ICollection<Order> Orders { get; set; }
}