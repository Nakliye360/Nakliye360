using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Nakliye360.Application.Abstractions.Services.Authentication;
using Nakliye360.Application.Exceptions.Authentication;
using Nakliye360.Application.Helpers;
using Nakliye360.Application.Models.DTOs.Authentication.RequestModel;
using Nakliye360.Application.Models.DTOs.Authentication.ResponseModel;
using Nakliye360.Domain.Entities.Account;
using System.Text;

namespace Nakliye360.Persistence.Services.Authentication;

public class AccountService : IAccountService
{
    #region Fields & Ctor
    private readonly UserManager<AppUser> _userManager;

    public AccountService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    #endregion

    #region Methods
    public async Task AssignRoleToUserAsnyc(string userId, string[] roles)
    {
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userRoles);

            await _userManager.AddToRolesAsync(user, roles);
        }
    }

    public async Task<RegisterUserResponseModel> CreateUserAsync(RegisterDto request)
    {
        IdentityResult result = await _userManager.CreateAsync(new AppUser()
        {
            Id = Guid.NewGuid().ToString(),
            UserName = request.Username,
            Email = request.Email,
        }, request.Password);

        RegisterUserResponseModel response = new RegisterUserResponseModel { Status = result.Succeeded };

        if (result.Succeeded)
            response.Message = "Kullanıcı başarılı bir şekilde oluşturuldu."; // User created successfully
        else
        {
            foreach (var error in result.Errors)
                response.Message += $"{error.Code} - {error.Description}\n";
        }

        return response;
    }

    public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
    {
        AppUser user = await _userManager.FindByIdAsync(userIdOrName);
        if (user == null)
            user = await _userManager.FindByNameAsync(userIdOrName);

        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles.ToArray();
        }
        return new string[] { };
    }

    public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
    {
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            resetToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetToken));
            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (result.Succeeded)
                await _userManager.UpdateSecurityStampAsync(user);
            else
                throw new PasswordChangeFailedException();
        }
    }

    public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
    {
        if (user != null)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
            await _userManager.UpdateAsync(user);
        }
        else
            throw new NotFoundUserException();
    }
    #endregion
}
