namespace ETicaret.Application.RequestParameters;

public class PaginationRequest
{
    public int PageSize { get; init; } = 10;
    public int Page { get; init; } = 0;
}