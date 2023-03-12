using ETicaret.Application.Features.Product.Responses;
using MediatR;

namespace ETicaret.Application.Features.Product.Requests;

public sealed class GetProductByIdRequest : IRequest<GetProductByIdResponse>
{
    public Guid Id { get; init; }
}