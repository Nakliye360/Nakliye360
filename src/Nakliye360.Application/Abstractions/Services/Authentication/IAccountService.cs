using Nakliye360.Application.Models.DTOs.Authentication.RequestModel;
using Nakliye360.Application.Models.DTOs.Authentication.ResponseModel;
using Nakliye360.Domain.Entities.Account;

namespace Nakliye360.Application.Abstractions.Services.Authentication;

public interface IAccountService
{
    Task<RegisterUserResponseModel> CreateUserAsync(RegisterDto request);
    Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
    Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
    Task AssignRoleToUserAsnyc(string userId, string[] roles);
    Task<string[]> GetRolesToUserAsync(string userIdOrName);
}
