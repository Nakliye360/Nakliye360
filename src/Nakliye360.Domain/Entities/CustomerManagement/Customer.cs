using Nakliye360.Domain.Entities.Account;
using Nakliye360.Domain.Enums;

namespace Nakliye360.Domain.Entities.CustomerManagement;

public class Customer : BaseEntity
{
    public string AppUserId { get; set; }
    public AppUser AppUser { get; set; } // Navigation

    public CustomerType CustomerType { get; set; } // Enums.CustomerType

    public string IdentityNumber { get; set; } // TC/VKN
    public string PhoneNumber { get; set; }
    public string? Email { get; set; } // zorunlu değil
    public string Address { get; set; }

    public IndividualCustomer? IndividualCustomer { get; set; }
    public CorporateCustomer? CorporateCustomer { get; set; }
    public ProducerCustomer? ProducerCustomer { get; set; }

}