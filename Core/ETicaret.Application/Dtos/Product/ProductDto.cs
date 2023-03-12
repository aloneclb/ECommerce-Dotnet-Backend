using ETicaret.Domain.Entities;

namespace ETicaret.Application.Dtos.Product;

public class ProductDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Stock { get; set; }
    public long Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IEnumerable<ProductImage> Images { get; set; }
}