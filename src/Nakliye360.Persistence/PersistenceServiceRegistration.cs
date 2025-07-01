using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nakliye360.Application.Abstractions.Services;
using Nakliye360.Application.Abstractions.Services.Authentication;
using Nakliye360.Application.Abstractions.Services.CustomerManagement;
using Nakliye360.Application.Abstractions.Services.RoleManagement;
using Nakliye360.Application.Repositories;
using Nakliye360.Domain.Entities.Account;
using Nakliye360.Domain.Entities.Role;
using Nakliye360.Persistence.Contexts;
using Nakliye360.Persistence.Repositories;
using Nakliye360.Persistence.Services.Authentication;
using Nakliye360.Persistence.Services.CustomerManagement;
using Nakliye360.Persistence.Services.RoleManagement;
using System.Security.Claims;
using System.Text;

namespace Nakliye360.Persistence;

public static class PersistenceServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddDbContext<Nakliye360DbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));



        #region Authentication Services
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequiredLength = 3;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
        }).AddEntityFrameworkStores<Nakliye360DbContext>()
        .AddDefaultTokenProviders();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        // Authentication Services
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IInternalAuthenticationService, AuthenticationService>();
        services.AddScoped<IExternalAuthenticationService, AuthenticationService>();

        #endregion Authentication End


        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IPermissionService, PermissionService>();


        services.AddScoped<ICustomerService, CustomerService>();


    }
}
