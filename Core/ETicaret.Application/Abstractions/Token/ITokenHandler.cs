using System.Security.Claims;
using ETicaret.Application.Dtos.Token;
using ETicaret.Domain.Entities.Identity;

namespace ETicaret.Application.Abstractions.Token;

public interface ITokenHandler
{
    TokenDto CreateAccessToken(Claim[] claims, int accessMinute, int refreshMinute);

    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}