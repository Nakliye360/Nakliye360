namespace Nakliye360.Application.Models.DTOs.RoleManagement;

public class AssignPermissionsRequest
{
    public string RoleId { get; set; }
    public List<string> PermissionCodes { get; set; }
}
