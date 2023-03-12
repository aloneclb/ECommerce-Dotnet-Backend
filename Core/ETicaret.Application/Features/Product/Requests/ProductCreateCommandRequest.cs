using ETicaret.Application.Features.Product.Responses;
using MediatR;

namespace ETicaret.Application.Features.Product.Requests;

public class ProductCreateCommandRequest : IRequest<ProductCreateCommandResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Stock { get; set; }
    public long Price { get; set; }
}