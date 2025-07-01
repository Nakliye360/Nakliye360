using Nakliye360.Domain.Entities.Role;

namespace Nakliye360.Persistence.Contexts.Seeder;

public static class RoleSeed
{
    public static List<AppRole> Roles => new()
    {
        new AppRole { Id = "admin", Name = "Admin", NormalizedName = "ADMIN" },
        new AppRole { Id = "operator", Name = "Operator", NormalizedName = "OPERATOR" }
    };

    public static List<RolePermission> RolePermissions => new()
    {
        // Admin tüm izinlere sahip
        new RolePermission { Id = 1, RoleId = "admin", PermissionId = 1 },
        new RolePermission { Id = 2, RoleId = "admin", PermissionId = 2 },
        new RolePermission { Id = 3, RoleId = "admin", PermissionId = 3 },
        new RolePermission { Id = 4, RoleId = "admin", PermissionId = 4 },

        // Operator sadece görüntüleyebilir
        new RolePermission { Id = 5, RoleId = "operator", PermissionId = 2 },
        new RolePermission { Id = 6, RoleId = "operator", PermissionId = 4 },
    };
}

