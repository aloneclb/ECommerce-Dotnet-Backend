using System.IdentityModel.Tokens.Jwt;
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

    public TokenDto CreateAccessToken(int minute)
    {
        TokenDto token = new();

        // SecurityKey'in simetriğini alıyoruz.
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTToken:SecurityKey"]!));
        // Tokenin neye göre şifreleneciğini oluşturuyoruz. Hangi algoritma ve hangi security key değeri vererek 
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        // Token geçerlilik süresini belirtiyoruz.
        token.Expiration = DateTime.UtcNow.AddMinutes(minute);

        // Tokeni oluşturuyoruz.
        JwtSecurityToken securityToken = new(
            audience: _configuration["JWTToken:Audience"],
            issuer: _configuration["JWTToken:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials
        );

        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        return token;
    }
}