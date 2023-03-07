using ETicaret.Application.Dtos.Product;
using FluentValidation;

namespace ETicaret.Application.Validators.Products;

public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequest>
{
    public ProductCreateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Lütfen ürün adını boş geçmeyiniz.")
            .MaximumLength(100)
                .WithMessage("Ürün adı 100 karakterden fazla olamaz.");
        
        RuleFor(x => x.Stock)
            .GreaterThan(0)
            .WithMessage("Ürün Stoğu 0'dan büyük olmalıdır.");
        
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Ürün fiyatı 0'dan büyük olmalıdır.");
    }
}