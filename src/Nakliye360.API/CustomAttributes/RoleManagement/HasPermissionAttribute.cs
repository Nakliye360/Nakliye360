using Microsoft.AspNetCore.Authorization;

namespace Nakliye360.API.CustomAttributes.RoleManagement;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permissionCode)
    {
        Policy = $"Permission:{permissionCode}";
    }
}
