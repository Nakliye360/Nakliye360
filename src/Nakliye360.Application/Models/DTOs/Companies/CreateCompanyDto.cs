namespace Nakliye360.Application.Models.DTOs.Companies;

public class CreateCompanyDto
{
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public string TaxNumber { get; set; }
    public string TaxAdministrator { get; set; }

}
