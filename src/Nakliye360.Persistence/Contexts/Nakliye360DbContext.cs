using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nakliye360.Application.Abstractions.Session;
using Nakliye360.Domain.Entities;
using Nakliye360.Domain.Entities.Account;
using Nakliye360.Domain.Entities.CustomerManagement;
using Nakliye360.Domain.Entities.DriverManagement;
using Nakliye360.Domain.Entities.Role;
using Nakliye360.Persistence.Contexts.Seeder;
using System.Security.Claims;

namespace Nakliye360.Persistence.Contexts;

public class Nakliye360DbContext : IdentityDbContext<AppUser, AppRole, string>
{
    private readonly ICurrentUserSession _currentUserSession;


    public Nakliye360DbContext(DbContextOptions<Nakliye360DbContext> options, ICurrentUserSession currentUserSession) : base(options)
    {
        //AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        //AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        _currentUserSession = currentUserSession;
    }


    #region RoleManagement
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    #endregion



    #region CustomerManagement 

    public DbSet<Customer> Customers { get; set; }
    public DbSet<IndividualCustomer> IndividualCustomers { get; set; }
    public DbSet<CorporateCustomer> CorporateCustomers { get; set; }
    public DbSet<ProducerCustomer> ProducerCustomers { get; set; }

    #endregion

    #region OrderManagement
    public DbSet<Domain.Entities.OrderManagement.Order> Orders { get; set; }
    public DbSet<Domain.Entities.OrderManagement.OrderItem> OrderItems { get; set; }
    #endregion

    #region VehicleManagement
    public DbSet<Domain.Entities.VehicleManagement.Vehicle> Vehicles { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    #endregion


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Nakliye360DbContext).Assembly);

        //modelBuilder.Entity<Permission>().HasData(PermissionSeed.Permissions);
        //modelBuilder.Entity<AppRole>().HasData(RoleSeed.Roles);
        //modelBuilder.Entity<RolePermission>().HasData(RoleSeed.RolePermissions);
        //modelBuilder.Entity<AppUser>().HasData(UserSeed.AdminUser);
        //modelBuilder.Entity<IdentityUserRole<string>>().HasData(UserRoleSeed.AdminUserRole);

    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var intEntities = ChangeTracker
            .Entries<BaseEntity>();

        foreach (var entity in intEntities)
        {

            if (entity.State == EntityState.Added)
            {
                entity.Entity.CreatedDate = DateTime.UtcNow;
                entity.Entity.CreatedBy = _currentUserSession.UserName ?? "System";
            }
            else if (entity.State == EntityState.Modified)
            {
                entity.Entity.ModifiedDate = DateTime.UtcNow;
                entity.Entity.ModifiedBy = _currentUserSession.UserName ?? "System";
            }
            else if (entity.State == EntityState.Deleted)
            {
                entity.Entity.DeletedDate = DateTime.UtcNow;
                entity.Entity.DeletedBy = _currentUserSession.UserName ?? "System"; 
                entity.Entity.IsDeleted = true;
                entity.State = EntityState.Modified;
            }

            //_ = entity.State switch
            //{
            //    EntityState.Added => entity.Entity.CreatedDate = DateTime.Now,
            //    EntityState.Modified => entity.Entity.UpdatedDate = DateTime.Now,
            //    EntityState.Deleted => entity.Entity.DeletedDate = DateTime.Now,
            //    _ => DateTime.Now
            //};
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

}
