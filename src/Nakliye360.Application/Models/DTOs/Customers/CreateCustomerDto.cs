using Nakliye360.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Nakliye360.Application.Models.DTOs.CustomerManagement;

public class CreateCustomerDto
{
    public string AppUserId { get; set; }

    public CustomerType CustomerType { get; set; }

    [Required]
    [StringLength(11, MinimumLength = 10)]
    public string IdentityNumber { get; set; } // TC veya Vergi No

    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Address { get; set; }

    // Bireysel müşteriler için
    public string FirstName { get; set; }
    public string LastName { get; set; }

    // Kurumsal müşteriler için
    public string CompanyName { get; set; }
    public string TaxNumber { get; set; }

    // Üretici müşteriler için (şimdilik kullanmıyoruz)
    //public string ProductionType { get; set; }
}