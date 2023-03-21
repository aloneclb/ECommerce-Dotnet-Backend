using ETicaret.Application.Dtos.Token;

namespace ETicaret.Application.Abstractions.Services;

public interface IAuthService
{
    Task<TokenDto> LoginAsync(string emailOrUsername, string password);
    TokenDto RefreshAsync(string refreshToken);
}