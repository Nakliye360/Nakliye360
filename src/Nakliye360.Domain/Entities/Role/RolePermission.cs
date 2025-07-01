namespace Nakliye360.Domain.Entities.Role;

public class RolePermission : BaseEntity
{
    public string RoleId { get; set; } 
    public AppRole Role { get; set; }

    public int PermissionId { get; set; }
    public Permission Permission { get; set; }
}
