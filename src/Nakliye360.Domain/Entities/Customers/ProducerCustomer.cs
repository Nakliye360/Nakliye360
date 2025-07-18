namespace Nakliye360.Domain.Entities.CustomerManagement;

public class ProducerCustomer : BaseEntity
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public string ProductionType { get; set; }
    public string CompanyName { get; set; } // Opsiyonel
}
