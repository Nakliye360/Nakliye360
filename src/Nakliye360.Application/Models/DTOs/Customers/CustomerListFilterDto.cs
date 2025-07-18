using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.CustomerManagement;

public class CustomerListFilterDto
{
    public string? Search { get; set; }
    public CustomerType? CustomerType { get; set; }
}
