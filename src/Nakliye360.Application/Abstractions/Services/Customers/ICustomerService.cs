using Nakliye360.Application.Models.DTOs.CustomerManagement;

namespace Nakliye360.Application.Abstractions.Services.CustomerManagement;

public interface ICustomerService
{
    Task<int> CreateCustomerAsync(CreateCustomerDto dto);
    Task<bool> UpdateCustomerAsync(UpdateCustomerDto dto);
    Task<CustomerDto> GetCustomerByIdAsync(int id);
    Task<List<CustomerDto>> GetCustomerListAsync(CustomerListFilterDto filter);
    Task<bool> DeleteCustomerAsync(int id);
    Task<bool> ExistsByIdentityNumberAsync(string identityNumber);

}
