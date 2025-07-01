using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nakliye360.Application.Abstractions.Services;
using Nakliye360.Application.Abstractions.Services.Authentication;
using Nakliye360.Application.Models.DTOs.RoleManagement;

namespace Nakliye360.API.Controllers.RoleManagement
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IAccountService _accountService;

        public RolesController(IRoleService roleService, IAccountService accountService)
        {
            _roleService = roleService;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            var roles = _roleService.GetAllRoles();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var role = await _roleService.GetRoleById(id);
            if (role == default)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRole request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Role name cannot be empty.");
            }
            bool result = await _roleService.CreateRole(request.Name);
            if (result)
            {
                return CreatedAtAction(nameof(GetRoleById), new { id = request.Name }, request.Name);
            }
            return BadRequest("Failed to create role.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(UpdateRoleDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Role name cannot be empty.");
            }
            bool result = await _roleService.UpdateRole(request.Id, request.Name);
            if (result)
            {
                return NoContent();
            }
            return NotFound("Role not found or update failed.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            bool result = await _roleService.DeleteRole(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound("Role not found or deletion failed.");
        }


        [HttpPost("assign")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserDto request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId) || request.Roles == null || request.Roles.Length == 0)
            {
                return BadRequest("User ID and roles cannot be empty.");
            }
            await _accountService.AssignRoleToUserAsnyc(request.UserId, request.Roles);
            return NoContent();
        }

        [HttpGet("user-roles/{userIdOrName}")]
        public async Task<IActionResult> GetRolesToUser(string userIdOrName)
        {
            if (string.IsNullOrWhiteSpace(userIdOrName))
            {
                return BadRequest("User ID or name cannot be empty.");
            }
            var roles = await _accountService.GetRolesToUserAsync(userIdOrName);
            if (roles == null || roles.Length == 0)
            {
                return NotFound("No roles found for the user.");
            }
            return Ok(roles);
        }
    }
}
