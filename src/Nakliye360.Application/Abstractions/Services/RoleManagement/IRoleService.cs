using Nakliye360.Application.Models.DTOs.RoleManagement;

namespace Nakliye360.Application.Abstractions.Services;

public interface IRoleService
{
    IEnumerable<RoleDto> GetAllRoles();
    Task<(string id, string name)> GetRoleById(string id);
    Task<bool> CreateRole(string name);
    Task<bool> DeleteRole(string id);
    Task<bool> UpdateRole(string id, string name);

}
