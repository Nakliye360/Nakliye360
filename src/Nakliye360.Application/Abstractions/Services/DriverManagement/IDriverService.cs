using Nakliye360.Application.Models.DTOs.DriverManagement;

namespace Nakliye360.Application.Abstractions.Services.DriverManagement
{
    public interface IDriverService
    {
        Task<List<DriverDto>> GetAllAsync();
        Task<DriverDto?> GetByIdAsync(int id);
        Task<int> CreateDriverAsync(CreateDriverDto dto);
        Task<string> UpdateDriverAsync(UpdateDriverDto dto);
        Task DeleteDriverAsync(int id);
    }
}
