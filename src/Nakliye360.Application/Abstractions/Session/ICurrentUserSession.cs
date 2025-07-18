using Nakliye360.Domain.Entities.Account;

namespace Nakliye360.Application.Abstractions.Session;

public interface ICurrentUserSession
{
    bool IsAuthenticated { get; }
    string? UserId { get; }
    string? UserName { get; }
    string? PhoneNumber { get; }
    string? PhoneNumberConfirmed { get; }
    string? Email { get; }
    string? EmailConfirmed { get; }
    List<string> Roles { get; }
    string? IpAddress { get; }
    HashSet<string> Permissions { get; set; } // 👈 Yetkiler için cache
    List<string> RoleIds { get; } // 👈 ROL ID'leri buraya eklendi
}
