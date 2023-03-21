using ETicaret.Application.Abstractions.Services;
using ETicaret.Application.Features.Auth.Requests;
using ETicaret.Application.Features.Auth.Responses;
using MediatR;

namespace ETicaret.Application.Features.Auth.Commands;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
{
    private readonly IAuthService _authService;

    public RefreshTokenCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request,
        CancellationToken cancellationToken)
    {
        return new RefreshTokenCommandResponse()
        {
            Token = _authService.RefreshAsync(request.RefreshToken)
        };
    }
}