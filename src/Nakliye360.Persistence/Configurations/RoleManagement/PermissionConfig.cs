using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nakliye360.Domain.Entities.Role;

namespace Nakliye360.Persistence.Configurations.RoleManagement;

public class PermissionConfig : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Code)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(p => p.Code)
               .IsUnique();

        builder.Property(p => p.Description)
               .HasMaxLength(250);

        builder.HasMany(p => p.RolePermissions)
               .WithOne(rp => rp.Permission)
               .HasForeignKey(rp => rp.PermissionId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

