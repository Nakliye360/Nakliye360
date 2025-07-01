namespace Nakliye360.Domain.Entities.CustomerManagement;

public class CorporateCustomer : BaseEntity
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public string CompanyName { get; set; }
    public string TaxNumber { get; set; }
}
