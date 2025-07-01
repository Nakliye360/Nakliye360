using Microsoft.Extensions.DependencyInjection;
using Nakliye360.Application.Abstractions.Services.Authentication;
using Nakliye360.Application.Abstractions.Session;
using Nakliye360.Infrastructure.Services.Authentication;
using Nakliye360.Infrastructure.Services.Session;

namespace Nakliye360.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped<ICurrentUserSession, CurrentUserSession>();
    }
}
