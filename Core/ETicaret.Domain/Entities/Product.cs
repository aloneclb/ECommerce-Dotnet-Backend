using ETicaret.Domain.Entities.Base;

namespace ETicaret.Domain.Entities;

public class Product : TimeStampEntity<Guid>
{
    public string Name { get; set; }
    public int Stock { get; set; }
    public long Price { get; set; }

    // Navigation Prop
    public ICollection<Order> Orders { get; set; }
    public ICollection<ProductImage> Images { get; set; }
}