using Microsoft.EntityFrameworkCore;
using Nakliye360.Application.Abstractions.Services.RoleManagement;
using Nakliye360.Application.Models.DTOs.RoleManagement;
using Nakliye360.Domain.Entities.Role;
using Nakliye360.Persistence.Contexts;
using System;

namespace Nakliye360.Persistence.Services.RoleManagement;

public class PermissionService : IPermissionService
{
    private readonly Nakliye360DbContext _context;

    public PermissionService(Nakliye360DbContext context)
    {
        _context = context;
    }

    public async Task<List<PermissionDto>> GetAllPermissionsAsync()
    {
        return await _context.Permissions
            .Select(p => new PermissionDto
            {
                Code = p.Code,
                Description = p.Description
            })
            .ToListAsync();
    }

    public async Task<List<string>> GetPermissionCodesByRoleAsync(string roleId)
    {
        return await _context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.Permission.Code)
            .ToListAsync();
    }

    public async Task<bool> AssignPermissionsToRoleAsync(string roleId, List<string> permissionCodes)
    {
        var role = await _context.Roles
            .Include(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(r => r.Id == roleId);

        if (role == null)
            return false;

        // Mevcut izinleri temizle
        role.RolePermissions.Clear();

        // Yeni izinleri veritabanından al
        var permissions = await _context.Permissions
            .Where(p => permissionCodes.Contains(p.Code))
            .ToListAsync();

        foreach (var permission in permissions)
        {
            role.RolePermissions.Add(new RolePermission
            {
                RoleId = role.Id,
                PermissionId = permission.Id
            });
        }

        await _context.SaveChangesAsync();
        return true;
    }
}

