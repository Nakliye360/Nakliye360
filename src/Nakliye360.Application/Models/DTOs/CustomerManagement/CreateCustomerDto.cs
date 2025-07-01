using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.CustomerManagement;

public class CreateCustomerDto
{
    public string AppUserId { get; set; }

    public CustomerType CustomerType { get; set; }

    public string IdentityNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string Address { get; set; }

    // Individual
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    // Corporate
    public string? CompanyName { get; set; }
    public string? TaxNumber { get; set; }

    // Producer
    public string? ProductionType { get; set; }
}