using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nakliye360.Domain.Entities.Account;
using Nakliye360.Domain.Entities.Role;
using Nakliye360.Persistence.Contexts;

namespace Nakliye360.Persistence.Seeder
{
    /// <summary>
    /// Seeds initial permissions, roles and admin user.  Updated to include offer management permissions and role assignments.
    /// </summary>
    public static class DbSeeder
    {
        public static async Task SeedAsync(Nakliye360DbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            // 1. Seed permissions if none exist
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
                    new() { Id = 20, Code = "LoadRequest.Delete", Description = "Yük talebini silme" },
                    // Offer permissions
                    new() { Id = 21, Code = "Offer.Create", Description = "Teklif oluşturma" },
                    new() { Id = 22, Code = "Offer.View", Description = "Teklifleri görüntüleme" },
                    new() { Id = 23, Code = "Offer.Edit", Description = "Teklif güncelleme veya kabul/red" },
                    new() { Id = 24, Code = "Offer.Delete", Description = "Teklif silme veya geri çekme" }
                };

                context.Permissions.AddRange(permissions);
                await context.SaveChangesAsync();
            }

            // 2. Ensure built-in roles exist
            var roles = new[] { "admin", "operator", "customer", "carrier" };
            foreach (var roleName in roles)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new AppRole { Name = roleName });
                }
            }

            // 3. Create an admin user if it doesn't exist
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

            // 4. Assign permissions to roles
            var allPermissions = await context.Permissions.ToListAsync();

            // Admin role: has all permissions
            var adminRole = await roleManager.FindByNameAsync("admin");
            await AssignPermissionsToRoleAsync(context, adminRole!.Id, allPermissions.Select(p => p.Id).ToList());

            // Operator role: manage vehicles, drivers, shipments, view and edit/delete load requests and offers
            var operatorRole = await roleManager.FindByNameAsync("operator");
            var operatorPermissionIds = new List<int>
            {
                // Vehicle permissions
                5, 6, 7, 8,
                // Driver permissions
                9, 10, 11, 12,
                // Shipment permissions
                13, 14, 15, 16,
                // LoadRequest permissions (view/edit/delete)
                18, 19, 20,
                // Offer permissions (view/edit/delete)
                22, 23, 24
            };
            await AssignPermissionsToRoleAsync(context, operatorRole!.Id, operatorPermissionIds);

            // Customer role: create/manage their own load requests and view/edit offers (accept/reject)
            var customerRole = await roleManager.FindByNameAsync("customer");
            var customerPermissionIds = new List<int>
            {
                // LoadRequest permissions
                17, 18, 19, 20,
                // Offers: customers can view and edit (accept/reject) offers
                22, 23
            };
            await AssignPermissionsToRoleAsync(context, customerRole!.Id, customerPermissionIds);

            // Carrier role: can view load requests and manage their offers
            var carrierRole = await roleManager.FindByNameAsync("carrier");
            var carrierPermissionIds = new List<int>
            {
                // LoadRequest.View so carriers can see available loads
                18,
                // Offer permissions (create/view/edit/delete)
                21, 22, 23, 24
            };
            await AssignPermissionsToRoleAsync(context, carrierRole!.Id, carrierPermissionIds);

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Helper method that ensures a role has exactly the provided permissions.  It adds missing permissions and removes obsolete ones.
        /// </summary>
        private static async Task AssignPermissionsToRoleAsync(Nakliye360DbContext context, string roleId, ICollection<int> permissionIds)
        {
            var existing = await context.RolePermissions
                .Where(rp => rp.RoleId == roleId)
                .ToListAsync();

            var toRemove = existing.Where(rp => !permissionIds.Contains(rp.PermissionId)).ToList();
            if (toRemove.Count > 0)
            {
                context.RolePermissions.RemoveRange(toRemove);
            }

            var toAdd = permissionIds.Where(pid => existing.All(rp => rp.PermissionId != pid)).ToList();
            foreach (var pid in toAdd)
            {
                context.RolePermissions.Add(new RolePermission { RoleId = roleId, PermissionId = pid });
            }
            // SaveChanges is called by the caller to reduce the number of database roundtrips
        }
    }
}