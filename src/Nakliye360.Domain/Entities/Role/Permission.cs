namespace Nakliye360.Domain.Entities.Role;

public class Permission : BaseEntity
{
    public string Code { get; set; }  // Örn: "Customer.Create"
    public string Description { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

}
