
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nakliye360.Application.Abstractions.Services;
using Nakliye360.Application.Models.DTOs.RoleManagement;
using Nakliye360.Domain.Entities.Role;

namespace Nakliye360.Persistence.Services.RoleManagement;

public class RoleService : IRoleService
{

    readonly RoleManager<AppRole> _roleManager;

    public RoleService(RoleManager<AppRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<bool> CreateRole(string name)
    {
        IdentityResult result = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid().ToString(), Name = name});

        return result.Succeeded;
    }

    public async Task<bool> DeleteRole(string id)
    {
        AppRole appRole = await _roleManager.FindByIdAsync(id);
        IdentityResult result = await _roleManager.DeleteAsync(appRole);
        return result.Succeeded;
    }

    public IEnumerable<RoleDto> GetAllRoles()
    {
        var query = _roleManager.Roles;

        var roles = query
            .Select(r => new RoleDto { Id = r.Id, Name = r.Name })
            .ToList();


        return (roles);
    }

    public async Task<(string id, string name)> GetRoleById(string id)
    {
        //var role = await _roleManager.GetRoleIdAsync(new() { Id = id});
        var item = await _roleManager.FindByIdAsync(id);

        return (item.Id, item.Name);
    }

    public async Task<bool> UpdateRole(string id, string name)
    {
        AppRole role = await _roleManager.FindByIdAsync(id);
        role.Name = name;
        IdentityResult result = await _roleManager.UpdateAsync(role);
        return result.Succeeded;
    }
}
