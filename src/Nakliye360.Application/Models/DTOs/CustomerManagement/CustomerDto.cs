using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.CustomerManagement;


public class CustomerDto
{
    public int Id { get; set; }

    public string AppUserId { get; set; }
    public string IdentityNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string Address { get; set; }

    public CustomerType CustomerType { get; set; }

    // Detail alanları (opsiyonel)
    public string? FullName { get; set; } // Individual için
    public string? CompanyName { get; set; } // Corporate & Producer için
    public string? TaxNumber { get; set; } // Corporate için
    public string? ProductionType { get; set; } // Producer için
}

