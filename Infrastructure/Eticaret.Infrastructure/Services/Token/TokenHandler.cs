using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ETicaret.Application.Abstractions.Token;
using ETicaret.Application.Dtos.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Eticaret.Infrastructure.Services.Token;

public class TokenHandler : ITokenHandler
{
    private readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenDto CreateAccessToken(Claim[] claims, int accessMinute, int refreshMinute)
    {
        TokenDto token = new();

        // SecurityKey'in simetriğini alıyoruz.
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTToken:SecurityKey"]!));
        // Tokenin neye göre şifreleneciğini oluşturuyoruz. Hangi algoritma ve hangi security key değeri vererek 
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        // Token geçerlilik süresini belirtiyoruz.
        token.AccessTokenExpiration = DateTime.UtcNow.AddSeconds(accessMinute);
        token.RefreshTokenExpiration = DateTime.UtcNow.AddMinutes(refreshMinute);

        // Tokeni oluşturuyoruz.
        JwtSecurityToken securityToken = new(
            audience: _configuration["JWTToken:Audience"],
            issuer: _configuration["JWTToken:Issuer"],
            claims: claims,
            expires: token.AccessTokenExpiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials
        );

        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        token.RefreshToken = CreateRefreshToken(claims, refreshMinute); 
        return token;
    }

    private string CreateRefreshToken(IEnumerable<Claim> claims, int refreshMinute)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTToken:SecurityKey"]!));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken securityToken = new(
            audience: _configuration["JWTToken:Audience"],
            issuer: _configuration["JWTToken:Issuer"],
            expires: DateTime.UtcNow.AddMinutes(refreshMinute),
            notBefore: DateTime.UtcNow,
            claims: claims,
            signingCredentials: signingCredentials
        );
        JwtSecurityTokenHandler tokenHandler = new();
        return tokenHandler.WriteToken(securityToken);
    }
    
    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenParams = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidAudience = _configuration["JWTToken:Audience"],
            ValidIssuer = _configuration["JWTToken:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTToken:SecurityKey"]!)),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null && expires > DateTime.UtcNow
        };
        
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenParams, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtToken ||
                !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
                return null;

            return principal;
        }
        catch (Exception)
        {
            return null;
        }
    }
}