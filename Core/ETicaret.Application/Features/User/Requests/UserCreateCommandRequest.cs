using ETicaret.Application.Features.User.Responses;
using MediatR;

namespace ETicaret.Application.Features.User.Requests;

public class UserCreateCommandRequest : IRequest<UserCreateCommandResponse>
{
    public string? NameSurname { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PasswordConfirm { get; set; }
}