using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Nakliye360.Application.Abstractions.Services.Authentication;
using Nakliye360.Application.Models.DTOs.Authentication;
using Nakliye360.Domain.Entities.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Nakliye360.Infrastructure.Services.Authentication;

public class TokenHandler : ITokenHandler
{
    #region Fields & Ctor
    private readonly IConfiguration _configuration;
    private readonly UserManager<AppUser> userManager;

    public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager)
    {
        _configuration = configuration;
        this.userManager = userManager;
    }
    #endregion

    #region Methods
    public async Task<Token> CreateAccessToken(int second, AppUser user)
    {
        Token token = new Token();
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]!));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        token.Expiration = DateTime.UtcNow.AddDays(second);

        // Kullanıcı rollerini al
        var roles = await userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Kullanıcı ID'si
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),  // Kullanıcı adı
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Kullanıcı ID'si
            new Claim(ClaimTypes.Name, user.UserName!), // Kullanıcı adı
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber!), // Kullanıcı telefon numarası
            new Claim(ClaimTypes.Email, user.Email!), // Kullanıcı e-posta adresi
            new Claim("EmailConfirmed", user.EmailConfirmed.ToString()), // E-posta doğrulama durumu
            new Claim("PhoneNumberConfirmed", user.PhoneNumberConfirmed.ToString()), // Telefon numarası doğrulama durumu
            new Claim("IpAddress", user.LastLoginIpAddress ?? string.Empty), // Kullanıcının son giriş IP adresi


            


        };

        // Rollerini ekleyelim
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role)); // Roller
        }

        JwtSecurityToken securityToken = new JwtSecurityToken(
            audience: _configuration["Token:Audience"],
            issuer: _configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials,
            claims: claims // Claimler burada
        );

        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);
        token.RefreshToken = CreateRefreshToken();
        return token;
    }

    public string CreateRefreshToken()
    {
        byte[] number = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);
    }

    public bool ValidateToken(string token)
    {
        if (string.IsNullOrEmpty(token))
            return false;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Token:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Token:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero // Token süresi dolduğunda hemen geçersiz olsun
            }, out SecurityToken validatedToken);

            return true;
        }
        catch
        {
            return false;
        }
    }

    #endregion
}
