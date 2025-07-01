using Microsoft.AspNetCore.Identity;

namespace Nakliye360.Domain.Entities.Role;

public class AppRole : IdentityRole
{
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

}
