using Microsoft.AspNetCore.Identity;
using Nakliye360.Domain.Entities.Account;

namespace Nakliye360.Persistence.Contexts.Seeder;

public static class UserSeed
{
    public static readonly string AdminUserId = "c37c47de-9f2e-4d7e-95c6-5c2a3b8d7bfa"; // sabit GUID

    public static AppUser AdminUser => new()
    {
        Id = AdminUserId,
        UserName = "admin",
        NormalizedUserName = "ADMIN",
        Email = "zng.caferaydin@gmail.com",
        NormalizedEmail = "ZNG.CAFERAYDIN@GMAIL.COM",
        EmailConfirmed = true,
        PasswordHash = new PasswordHasher<AppUser>().HashPassword(null, "123qwe"),
        SecurityStamp = Guid.NewGuid().ToString("D")
    };
}
public static class UserRoleSeed
{
    public static IdentityUserRole<string> AdminUserRole => new()
    {
        RoleId = "admin", // string roleId
        UserId = UserSeed.AdminUserId
    };
}


