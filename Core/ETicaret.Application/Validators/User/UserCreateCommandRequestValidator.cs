using ETicaret.Application.Features.User.Requests;
using FluentValidation;

namespace ETicaret.Application.Validators.User;

public class UserCreateCommandRequestValidator : AbstractValidator<UserCreateCommandRequest>
{
    public UserCreateCommandRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull();
        RuleFor(x => x.NameSurname).NotEmpty().NotNull();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}