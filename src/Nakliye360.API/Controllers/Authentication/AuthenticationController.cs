using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Nakliye360.API.CustomAttributes.RoleManagement;
using Nakliye360.Application.Abstractions.Services.Authentication;
using Nakliye360.Application.Abstractions.Session;
using Nakliye360.Application.Exceptions.Authentication;
using Nakliye360.Application.Models.DTOs.Authentication;
using Nakliye360.Application.Models.DTOs.Authentication.RequestModel;
using Nakliye360.Application.Models.DTOs.Authentication.ResponseModel;
using Nakliye360.Domain.Entities.Account;
using System.Threading.Tasks;

namespace Nakliye360.API.Controllers.Authentication;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IAccountService _accountService;
    private readonly ICurrentUserSession _currentUserSession;
    private readonly UserManager<AppUser> _userManager;
    public AuthenticationController(IAuthenticationService authenticationService, IAccountService accountService, ICurrentUserSession currentUserSession, UserManager<AppUser> userManager)
    {
        _authenticationService = authenticationService;
        _accountService = accountService;
        _currentUserSession = currentUserSession;
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _accountService.CreateUserAsync(request);
        //return Ok(result);
        return CreatedAtAction(nameof(Register), new { id = result.Message }, result);

    }

    [HttpPost("password-reset")]
    public async Task<IActionResult> PasswordReset([FromBody] PasswordResetRequestModel request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _authenticationService.PasswordResetAsnyc(request);
        return Ok("Password reset successfully.");

    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            Token token = await _authenticationService.LoginAsync(request.UsernameOrEmail, request.Password, 900, _currentUserSession.IpAddress);
            return Ok(token);
        }
        catch (NotFoundUserException)
        {
            return NotFound("User not found");
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequestModel request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Token token = await _authenticationService.GoogleLoginAsync(request.IdToken, request.AccessTokenLifeTime);
        return Ok(token);

    }

    [HttpPost("facebook-login")]
    public async Task<IActionResult> FacebookLogin([FromBody] FacebookLoginRequestModel request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Token token = await _authenticationService.FacebookLoginAsync(request.AuthToken, request.AccessTokenLifeTime);
        return Ok(token);

    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestModel request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Token token = await _authenticationService.RefreshTokenLoginAsync(request.RefreshToken);
        return Ok(token);


    }

    [Authorize]
    [HttpPost("update-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDto request)
    {
        var userId = _currentUserSession.UserId;

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _accountService.UpdatePasswordAsync(userId, request.ResetToken, request.Password);
        return Ok("Password updated successfully.");

    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult Me()
    {
        UserProfileResponseModel appuser = new UserProfileResponseModel
        {
            UserName = _currentUserSession.UserName,
            Phone = _currentUserSession.PhoneNumber,
            PhoneConfirmed = _currentUserSession.PhoneNumberConfirmed == "True" ? true : false,
            Email = _currentUserSession.Email,
            EmailConfirmed = _currentUserSession.EmailConfirmed == "True" ? true : false,
        };

        return Ok(appuser);
    }


}
