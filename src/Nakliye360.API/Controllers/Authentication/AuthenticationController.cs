using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nakliye360.Application.Abstractions.Services.Authentication;
using Nakliye360.Application.Exceptions.Authentication;
using Nakliye360.Application.Models.DTOs.Authentication;
using Nakliye360.Application.Models.DTOs.Authentication.RequestModel;

namespace Nakliye360.API.Controllers.Authentication;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IAccountService _accountService;
    public AuthenticationController(IAuthenticationService authenticationService, IAccountService accountService)
    {
        _authenticationService = authenticationService;
        _accountService = accountService;
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
            Token token = await _authenticationService.LoginAsync(request.UsernameOrEmail, request.Password, 900);
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

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _accountService.UpdatePasswordAsync(request.UserId, request.ResetToken, request.Password);
        return Ok("Password updated successfully.");

    }

}
