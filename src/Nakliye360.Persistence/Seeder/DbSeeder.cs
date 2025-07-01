using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nakliye360.Domain.Entities.Account;
using Nakliye360.Domain.Entities.Role;
using Nakliye360.Persistence.Contexts;

namespace Nakliye360.Persistence.Seeder;

public static class DbSeeder
{
    public static async Task SeedAsync(Nakliye360DbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        // 1. Permission kontrolü
        if (!await context.Permissions.AnyAsync())
        {
            var permissions = new List<Permission>
            {
                new() { Id = 1, Code = "Customer.Create", Description = "Müşteri oluşturma" },
                new() { Id = 2, Code = "Customer.View", Description = "Müşteri görüntüleme" },
                new() { Id = 3, Code = "Customer.Delete", Description = "Müşteri silme" },
                new() { Id = 4, Code = "Order.View", Description = "Siparişleri görme" }
            };

            context.Permissions.AddRange(permissions);
            await context.SaveChangesAsync();
        }

        // 2. Roller
        var roles = new[] { "admin", "operator" };

        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new AppRole { Name = roleName });
            }
        }

        // 3. Admin kullanıcı
        var adminEmail = "zng.caferaydin@gmail.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser is null)
        {
            var user = new AppUser
            {
                UserName = "admin",
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, "123qwe");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "admin");
            }
        }

        // 4. RolePermission – sadece admin için
        var adminRole = await roleManager.FindByNameAsync("admin");
        var existingRolePermissions = await context.RolePermissions
            .Where(x => x.RoleId == adminRole.Id)
            .Select(x => x.PermissionId)
            .ToListAsync();

        var allPermissions = await context.Permissions.ToListAsync();
        var newAssignments = allPermissions
            .Where(p => !existingRolePermissions.Contains(p.Id))
            .Select(p => new RolePermission
            {
                RoleId = adminRole.Id,
                PermissionId = p.Id
            });

        context.RolePermissions.AddRange(newAssignments);
        await context.SaveChangesAsync();
    }
}
