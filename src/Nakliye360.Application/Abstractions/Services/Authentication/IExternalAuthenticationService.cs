using Nakliye360.Application.Models.DTOs.Authentication;

namespace Nakliye360.Application.Abstractions.Services.Authentication;

public interface IExternalAuthenticationService
{
    Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime);
    Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
}
