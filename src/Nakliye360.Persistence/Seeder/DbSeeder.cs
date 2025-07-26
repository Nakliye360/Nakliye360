using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nakliye360.Domain.Entities.Account;
using Nakliye360.Domain.Entities.Role;
using Nakliye360.Persistence.Contexts;

namespace Nakliye360.Persistence.Seeder;

/// <summary>
/// Seeds initial permissions, roles and admin user.  Updated to include vehicle, driver, shipment and load request permissions.
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
                new() { Id = 16, Code = "Shipment.Delete", Description = "Sevkiyat silme" },
                // LoadRequest permissions
                new() { Id = 17, Code = "LoadRequest.Create", Description = "Yük talebi oluşturma" },
                new() { Id = 18, Code = "LoadRequest.View", Description = "Yük taleplerini görüntüleme" },
                new() { Id = 19, Code = "LoadRequest.Edit", Description = "Yük talebini güncelleme" },
                new() { Id = 20, Code = "LoadRequest.Delete", Description = "Yük talebini silme" }
            };

            context.Permissions.AddRange(permissions);
            await context.SaveChangesAsync();
        }

        // 2. Roles
        // Define built-in roles. Admin and operator already exist; we also introduce customer (yük sahibi) and carrier (araç sahibi)
        var roles = new[] { "admin", "operator", "customer", "carrier" };

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

        // 4. RolePermission – assign permissions to roles
        var allPermissions = await context.Permissions.ToListAsync();

        // Admin role: all permissions
        var adminRole = await roleManager.FindByNameAsync("admin");
        await AssignPermissionsToRoleAsync(context, adminRole!.Id, allPermissions.Select(p => p.Id).ToList());

        // Operator role: allow creating and managing shipments, drivers, vehicles and viewing/editing/deleting load requests
        var operatorRole = await roleManager.FindByNameAsync("operator");
        var operatorPermissionIds = new List<int>
        {
            // Vehicle permissions
            5, 6, 7, 8,
            // Driver permissions
            9, 10, 11, 12,
            // Shipment permissions
            13, 14, 15, 16,
            // LoadRequest permissions (operator can view, edit and delete)
            18, 19, 20
        };
        await AssignPermissionsToRoleAsync(context, operatorRole!.Id, operatorPermissionIds);

        // Customer role: allow creating and managing own load requests
        var customerRole = await roleManager.FindByNameAsync("customer");
        var customerPermissionIds = new List<int> { 17, 18, 19, 20 };
        await AssignPermissionsToRoleAsync(context, customerRole!.Id, customerPermissionIds);

        // Carrier role: allow viewing load requests only
        var carrierRole = await roleManager.FindByNameAsync("carrier");
        var carrierPermissionIds = new List<int> { 18 };
        await AssignPermissionsToRoleAsync(context, carrierRole!.Id, carrierPermissionIds);

        await context.SaveChangesAsync();
    }

    // Change the type of roleId parameter in AssignPermissionsToRoleAsync method to string
    private static async Task AssignPermissionsToRoleAsync(Nakliye360DbContext context, string roleId, ICollection<int> permissionIds)
    {
        // Fetch existing mappings from the database for the given role
        var existing = await context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .ToListAsync();

        // Determine which mappings should be removed because they are not in the desired set
        var toRemove = existing
            .Where(rp => !permissionIds.Contains(rp.PermissionId))
            .ToList();
        if (toRemove.Count > 0)
        {
            context.RolePermissions.RemoveRange(toRemove);
        }

        // Determine which permissions are missing and need to be added
        var toAdd = permissionIds
            .Where(pid => existing.All(rp => rp.PermissionId != pid))
            .ToList();
        foreach (var pid in toAdd)
        {
            context.RolePermissions.Add(new RolePermission
            {
                RoleId = roleId,
                PermissionId = pid
            });
        }
        // Note: Do not call SaveChanges here; caller is responsible for persisting changes.
    }
}
