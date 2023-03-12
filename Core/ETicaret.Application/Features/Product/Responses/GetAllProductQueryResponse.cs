namespace ETicaret.Application.Features.Product.Responses;

public class GetAllProductQueryResponse
{
    public int TotalCount { get; set; }
    public object Products { get; set; }
}