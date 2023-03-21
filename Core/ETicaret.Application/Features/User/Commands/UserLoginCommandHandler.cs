using ETicaret.Application.Abstractions.Services;
using ETicaret.Application.Features.User.Requests;
using ETicaret.Application.Features.User.Responses;
using MediatR;

namespace ETicaret.Application.Features.User.Commands;

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommandRequest, UserLoginCommandResponse>
{
    private readonly IAuthService _authService;

    public UserLoginCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<UserLoginCommandResponse> Handle(UserLoginCommandRequest request,
        CancellationToken cancellationToken)
    {
        return new UserLoginCommandResponse()
        {
            Token = await _authService.LoginAsync(request.EmailorUserName!, request.Password!)
        };
    }
}