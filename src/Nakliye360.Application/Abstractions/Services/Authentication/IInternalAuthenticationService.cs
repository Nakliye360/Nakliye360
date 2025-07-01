using Nakliye360.Application.Models.DTOs.Authentication;

namespace Nakliye360.Application.Abstractions.Services.Authentication;

public interface IInternalAuthenticationService
{
    Task<Token> LoginAsync(string usernameOrEmail, string password, int? accessTokenLifeTime);
    Task<Token> RefreshTokenLoginAsync(string refreshToken);
    Task<bool> Logout(string userId);
}
