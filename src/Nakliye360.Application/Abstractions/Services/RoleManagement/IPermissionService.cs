using Nakliye360.Application.Models.DTOs.RoleManagement;

namespace Nakliye360.Application.Abstractions.Services.RoleManagement;

public interface IPermissionService
{
    Task<List<PermissionDto>> GetAllPermissionsAsync();
    Task<List<string>> GetPermissionCodesByRoleAsync(string roleId);
    Task<bool> AssignPermissionsToRoleAsync(string roleId, List<string> permissionCodes);
}
