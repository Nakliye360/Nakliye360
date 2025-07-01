using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nakliye360.Application.Abstractions.Services.RoleManagement;
using Nakliye360.Application.Models.DTOs.RoleManagement;

namespace Nakliye360.API.Controllers.RoleManagement;

[ApiController]
[Route("api/[controller]")]
public class PermissionController : ControllerBase
{
    private readonly IPermissionService _permissionService;

    public PermissionController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPermissions()
    {
        var result = await _permissionService.GetAllPermissionsAsync();
        return Ok(result);
    }

    [HttpGet("role/{roleId}")]
    public async Task<IActionResult> GetPermissionsByRole(string roleId)
    {
        var result = await _permissionService.GetPermissionCodesByRoleAsync(roleId);
        return Ok(result);
    }

    [HttpPost("assign-to-role")]
    public async Task<IActionResult> AssignPermissionsToRole([FromBody] AssignPermissionsRequest request)
    {
        var success = await _permissionService.AssignPermissionsToRoleAsync(request.RoleId, request.PermissionCodes);
        if (success)
            return Ok(new { message = "Permissions assigned successfully." });

        return BadRequest("Failed to assign permissions.");
    }
}
