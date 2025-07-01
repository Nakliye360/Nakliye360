using Nakliye360.Domain.Entities.Role;

namespace Nakliye360.Persistence.Contexts.Seeder;

public static class PermissionSeed
{
    public static List<Permission> Permissions => new()
{
    new Permission { Id = 1, Code = "Customer.Create", Description = "Müşteri oluşturma" },
    new Permission { Id = 2, Code = "Customer.View", Description = "Müşteri görüntüleme" },
    new Permission { Id = 3, Code = "Customer.Delete", Description = "Müşteri silme" },
    new Permission { Id = 4, Code = "Order.View", Description = "Siparişleri görme" }
};
}
