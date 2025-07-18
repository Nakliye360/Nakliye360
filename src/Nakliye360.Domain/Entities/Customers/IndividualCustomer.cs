namespace Nakliye360.Domain.Entities.CustomerManagement;

public class IndividualCustomer : BaseEntity
{
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
}
