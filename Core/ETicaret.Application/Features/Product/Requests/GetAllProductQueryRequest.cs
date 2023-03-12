using ETicaret.Application.Features.Product.Responses;
using ETicaret.Application.RequestParameters;
using MediatR;

namespace ETicaret.Application.Features.Product.Requests;

public class GetAllProductQueryRequest : PaginationRequest, IRequest<GetAllProductQueryResponse>
{
}