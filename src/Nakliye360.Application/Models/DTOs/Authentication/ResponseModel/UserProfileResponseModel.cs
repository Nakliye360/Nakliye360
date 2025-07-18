namespace Nakliye360.Application.Models.DTOs.Authentication.ResponseModel;

public class UserProfileResponseModel
{
    public string UserName { get; set; }
    public string Phone { get; set; }
    public bool PhoneConfirmed { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string IpAddress { get; set; }
    public List<string> Roles { get; set; }
    public HashSet<string> Permissions { get; set; } // 👈 Yetkiler için cache


}
