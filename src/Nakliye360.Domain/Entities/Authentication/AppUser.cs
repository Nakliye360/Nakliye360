using Microsoft.AspNetCore.Identity;

namespace Nakliye360.Domain.Entities.Account;

public class AppUser : IdentityUser
{
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenEndDate { get; set; }
}
