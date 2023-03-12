using MediatR;
using Microsoft.AspNetCore.Http;

namespace ETicaret.Application.Features.ProductImage.Requests;

public class AddImageForProductRequest : IRequest<bool>
{
    public Guid Id { get; set; }
    public IList<IFormFile>? Images { get; set; }
}