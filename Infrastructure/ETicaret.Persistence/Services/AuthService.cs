using System.Security.Claims;
using ETicaret.Application.Abstractions.Services;
using ETicaret.Application.Abstractions.Token;
using ETicaret.Application.Dtos.Token;
using ETicaret.Application.Exceptions;
using ETicaret.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ETicaret.Persistence.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }

    public async Task<TokenDto> LoginAsync(string emailOrUsername, string password)
    {
        var user = await _userManager.FindByEmailAsync(emailOrUsername)
                   ?? await _userManager.FindByNameAsync(emailOrUsername);

        if (user is null)
            throw new NotFoundUserException();
        
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty)
        };

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
            throw new NotFoundUserException();

        return _tokenHandler.CreateAccessToken(claims, 15, 30);
    }

    public TokenDto RefreshAsync(string refreshToken)
    {
        // Kullanıcıyı bul
        var principal = _tokenHandler.GetPrincipalFromExpiredToken(refreshToken);
        if (principal is null)
            throw new NotImplementedException();

        var email = principal.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

        foreach (var claim in principal.Claims)
        {
            Console.WriteLine(claim.Type);
            Console.WriteLine(claim.Value);
        }
        
        
        if (string.IsNullOrEmpty(email))
            throw new NotImplementedException();
        
        
        var userId = principal.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
        if (string.IsNullOrEmpty(userId))
            throw new NotImplementedException();
        
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.Email, email)
        };
        
        return _tokenHandler.CreateAccessToken(claims, 10, 60);
    }

}