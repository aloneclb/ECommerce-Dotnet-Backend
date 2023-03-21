using ETicaret.Application.Dtos.Token;

namespace ETicaret.Application.Features.Auth.Responses;

public class RefreshTokenCommandResponse
{
    public TokenDto Token { get; set; }
}