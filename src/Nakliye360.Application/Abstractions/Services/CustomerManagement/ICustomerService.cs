using Nakliye360.Application.Models.DTOs.CustomerManagement;

namespace Nakliye360.Application.Abstractions.Services.CustomerManagement;

public interface ICustomerService
{
    Task<List<CustomerDto>> GetAllAsync();
    Task<int> CreateCustomerAsync(CreateCustomerDto dto);
    Task<string> UpdateCustomerAsync(UpdateCustomerDto request);
    Task DeleteCustomerAsync(int customerId);
    Task<CustomerDto?> GetCustomerByIdAsync(int customerId);
    Task<bool> ExistsByIdentityNumberAsync(string identityNumber);

}
