using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nakliye360.Application.Abstractions.Services.Authentication;
using Nakliye360.Application.Exceptions.Authentication;
using Nakliye360.Application.Helpers;
using Nakliye360.Application.Models.DTOs.Authentication;
using Nakliye360.Application.Models.DTOs.Authentication.RequestModel;
using Nakliye360.Domain.Entities.Account;

namespace Nakliye360.Persistence.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{

    #region Fields & Ctor
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IAccountService _accountService;
    private readonly ITokenHandler _tokenHandler;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAccountService accountService, ITokenHandler tokenHandler, ILogger<AuthenticationService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _accountService = accountService;
        _tokenHandler = tokenHandler;
        _logger = logger;
    }

    #endregion

    #region Methods
    public Task<Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime)
    {
        throw new NotImplementedException();
    }

    public Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime)
    {
        throw new NotImplementedException();
    }

    public async Task<Token> LoginAsync(string usernameOrEmail, string password, int? accessTokenLifeTime)
    {
        AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
        if (user == null)
            user = await _userManager.FindByEmailAsync(usernameOrEmail);

        if (user == null)
            throw new NotFoundUserException();


        SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

        if (result.Succeeded)
        {
            Token token = await _tokenHandler.CreateAccessToken(90, user);
            await _accountService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 120);
            return token;
        }

        throw new AuthenticationErrorException();
    }

    public async Task PasswordResetAsnyc(PasswordResetRequestModel requestModel)  // TODO
    {
        AppUser user = await _userManager.FindByEmailAsync(requestModel.Email);
        if (user != null)
        {
            string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            //byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
            //resetToken = WebEncoders.Base64UrlEncode(tokenBytes);
            resetToken = resetToken.UrlEncode();

            //await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken); 
        }
    }

    public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
    {
        AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
        {
            Token token = await _tokenHandler.CreateAccessToken(15, user);
            await _accountService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.Expiration, 300);
            return token;
        }
        else
            throw new NotFoundUserException();
    }

    public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
    {
        AppUser user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            //byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
            //resetToken = Encoding.UTF8.GetString(tokenBytes);
            resetToken = resetToken.UrlDecode();

            return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
        }
        return false;
    }

    public async Task<bool> Logout(string userId)
    {
        AppUser? user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            user.RefreshToken = null;
            user.RefreshTokenEndDate = DateTime.Now;
            IdentityResult result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        return false;
    }

    #endregion
}
