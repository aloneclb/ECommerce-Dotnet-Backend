using ETicaret.Application.Features.Product.Requests;
using FluentValidation;

namespace ETicaret.Application.Validators.Products;

public class GetAllProductQueryRequestValidator : AbstractValidator<GetAllProductQueryRequest>
{
    public GetAllProductQueryRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(0).WithMessage("Page değeri en az 0 olabilir.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize değeri en az 1 olmalıdır.")
            .LessThanOrEqualTo(100).WithMessage("PageSize değeri en fazla 100 olabilir.");
    }
}