namespace ETicaret.Application.Dtos.Product;

public class ProductCreateRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public long Price { get; set; }
}