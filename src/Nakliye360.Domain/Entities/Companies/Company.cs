using Nakliye360.Domain.Entities.Account;

namespace Nakliye360.Domain.Entities.Companies;

public class Company : BaseEntity
{
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public string TaxNumber { get; set; }
    public string TaxAdministrator { get; set; }

    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; }

}
