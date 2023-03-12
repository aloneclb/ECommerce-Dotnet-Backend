using ETicaret.Application.Features.Product.Requests;
using ETicaret.Application.Features.Product.Responses;
using ETicaret.Application.Repositories.Product;
using MediatR;

namespace ETicaret.Application.Features.Product.Commands;

public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommandRequest, ProductCreateCommandResponse>
{
    private readonly IProductWriteRepository _productWriteRepository;

    public ProductCreateCommandHandler(IProductWriteRepository productWriteRepository)
    {
        _productWriteRepository = productWriteRepository;
    }

    public async Task<ProductCreateCommandResponse> Handle(ProductCreateCommandRequest request,
        CancellationToken cancellationToken)
    {
        var product = new Domain.Entities.Product()
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };

        var result = await _productWriteRepository.AddAsync(product);
        await _productWriteRepository.SaveChanges();

        return new ProductCreateCommandResponse()
        {
            Result = result
        };
    }
}