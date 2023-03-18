using ETicaret.Application.Dtos.Token;

namespace ETicaret.Application.Features.User.Responses;

public class UserLoginCommandResponse
{
    public TokenDto Token { get; set; }
}