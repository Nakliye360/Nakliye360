using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.CustomerManagement;


public class CustomerDto
{
    public int Id { get; set; }

    public string AppUserId { get; set; }
    public CustomerType CustomerType { get; set; }

    public string IdentityNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string Address { get; set; }

    public IndividualCustomerDto? IndividualCustomer { get; set; }
    public CorporateCustomerDto? CorporateCustomer { get; set; }
    public ProducerCustomerDto? ProducerCustomer { get; set; }
}

