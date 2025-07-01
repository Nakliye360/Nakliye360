using Microsoft.AspNetCore.Authorization;
using Nakliye360.Application.Abstractions.Services.RoleManagement;
using Nakliye360.Application.Abstractions.Session;

namespace Nakliye360.API.CustomAttributes.RoleManagement;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly ICurrentUserSession _currentUserSession;
    private readonly IPermissionService _permissionService;

    public PermissionAuthorizationHandler(ICurrentUserSession currentUserSession, IPermissionService permissionService)
    {
        _currentUserSession = currentUserSession;
        _permissionService = permissionService;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (_currentUserSession.UserId == null)
        {
            context.Fail();
            return;
        }

        var roleIds = _currentUserSession.RoleIds;
        if (roleIds == null || !roleIds.Any())
        {
            context.Fail();
            return;
        }

        var allPermissions = new HashSet<string>();

        foreach (var roleId in roleIds)
        {
            var permissions = await _permissionService.GetPermissionCodesByRoleAsync(roleId);
            foreach (var permission in permissions)
                allPermissions.Add(permission);
        }

        if (allPermissions.Contains(requirement.PermissionCode))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}
