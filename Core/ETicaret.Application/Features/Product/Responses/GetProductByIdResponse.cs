using ETicaret.Application.Dtos.Product;

namespace ETicaret.Application.Features.Product.Responses;

public class GetProductByIdResponse
{
    public ProductDto? Product { get; set; }
}