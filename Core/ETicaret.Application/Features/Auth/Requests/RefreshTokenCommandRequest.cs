using ETicaret.Application.Features.Auth.Responses;
using MediatR;

namespace ETicaret.Application.Features.Auth.Requests;

public class RefreshTokenCommandRequest : IRequest<RefreshTokenCommandResponse>
{
    public string RefreshToken { get; set; }
}