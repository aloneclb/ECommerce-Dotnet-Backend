using ETicaret.Application.Features.User.Responses;
using MediatR;

namespace ETicaret.Application.Features.User.Requests;

public class UserLoginCommandRequest : IRequest<UserLoginCommandResponse>
{
    public string? EmailorUserName { get; set; }
    public string? Password { get; set; }
}