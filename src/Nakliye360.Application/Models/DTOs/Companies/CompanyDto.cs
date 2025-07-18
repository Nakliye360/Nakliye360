using Nakliye360.Domain.Entities.Account;

namespace Nakliye360.Application.Models.DTOs.Companies;

public class CompanyDto
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public string TaxNumber { get; set; }
    public string TaxAdministrator { get; set; }
    public string AppUserId { get; set; }

}
