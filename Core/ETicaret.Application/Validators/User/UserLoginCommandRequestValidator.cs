using ETicaret.Application.Features.User.Requests;
using FluentValidation;

namespace ETicaret.Application.Validators.User;

public class UserLoginCommandRequestValidator : AbstractValidator<UserLoginCommandRequest>
{
    public UserLoginCommandRequestValidator()
    {
        RuleFor(x => x.EmailorUserName).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}