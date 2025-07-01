namespace Nakliye360.Application.Models.DTOs.RoleManagement;

public class AssignRoleToUserDto
{
    public required string UserId { get; set; }
    public required string[] Roles { get; set; }
}
