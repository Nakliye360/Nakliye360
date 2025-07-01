using Nakliye360.Application.Models.DTOs.Authentication;
using Nakliye360.Domain.Entities.Account;

namespace Nakliye360.Application.Abstractions.Services.Authentication;

public interface ITokenHandler
{
    Task<Token> CreateAccessToken(int second, AppUser appUser);
    string CreateRefreshToken();
    bool ValidateToken(string token);
}
