using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nakliye360.Domain.Entities.Account;
using Nakliye360.Domain.Entities.Role;
using Nakliye360.Persistence.Contexts;

namespace Nakliye360.Persistence.Seeder;

/// <summary>
/// Seeds initial permissions, roles and admin user. Updated to include vehicle, driver and shipment permissions.
/// </summary>
public static class DbSeeder
{
    public static async Task SeedAsync(Nakliye360DbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        // 1. Permission control
        if (!await context.Permissions.AnyAsync())
        {
            var permissions = new List<Permission>
            {
                // Customer permissions
                new() { Id = 1, Code = "Customer.Create", Description = "Müşteri oluşturma" },
                new() { Id = 2, Code = "Customer.View", Description = "Müşteri görüntüleme" },
                new() { Id = 3, Code = "Customer.Delete", Description = "Müşteri silme" },
                // Order permissions
                new() { Id = 4, Code = "Order.View", Description = "Siparişleri görme" },
                // Vehicle permissions
                new() { Id = 5, Code = "Vehicle.Create", Description = "Araç oluşturma" },
                new() { Id = 6, Code = "Vehicle.View", Description = "Araç görüntüleme" },
                new() { Id = 7, Code = "Vehicle.Edit", Description = "Araç güncelleme" },
                new() { Id = 8, Code = "Vehicle.Delete", Description = "Araç silme" },
                // Driver permissions
                new() { Id = 9, Code = "Driver.Create", Description = "Sürücü oluşturma" },
                new() { Id = 10, Code = "Driver.View", Description = "Sürücü görüntüleme" },
                new() { Id = 11, Code = "Driver.Edit", Description = "Sürücü güncelleme" },
                new() { Id = 12, Code = "Driver.Delete", Description = "Sürücü silme" },
                // Shipment permissions
                new() { Id = 13, Code = "Shipment.Create", Description = "Sevkiyat oluşturma" },
                new() { Id = 14, Code = "Shipment.View", Description = "Sevkiyat görüntüleme" },
                new() { Id = 15, Code = "Shipment.Edit", Description = "Sevkiyat güncelleme" },
                new() { Id = 16, Code = "Shipment.Delete", Description = "Sevkiyat silme" }
            };

            context.Permissions.AddRange(permissions);
            await context.SaveChangesAsync();
        }

        // 2. Roles
        var roles = new[] { "admin", "operator" };

        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new AppRole { Name = roleName });
            }
        }

        // 3. Admin user
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

        // 4. RolePermission – assign all permissions to admin
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