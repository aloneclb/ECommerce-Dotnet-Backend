using ETicaret.Application.Dtos.Token;

namespace ETicaret.Application.Abstractions.Token;

public interface ITokenHandler
{
    TokenDto CreateAccessToken(int minute);
}