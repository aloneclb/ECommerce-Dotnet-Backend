using System.Security.Claims;
using ETicaret.Application.Dtos.Token;

namespace ETicaret.Application.Abstractions.Token;

public interface ITokenHandler
{
    TokenDto CreateAccessToken(Claim[] claims, int accessMinute, int refreshMinute);
    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}