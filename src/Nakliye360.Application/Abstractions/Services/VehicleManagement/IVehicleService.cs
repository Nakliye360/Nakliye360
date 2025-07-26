using Nakliye360.Application.Models.DTOs.VehicleManagement;

namespace Nakliye360.Application.Abstractions.Services.VehicleManagement
{
    public interface IVehicleService
    {
        Task<List<VehicleDto>> GetAllAsync();
        Task<VehicleDto?> GetByIdAsync(int id);
        Task<int> CreateVehicleAsync(CreateVehicleDto dto);
        Task<string> UpdateVehicleAsync(UpdateVehicleDto dto);
        Task DeleteVehicleAsync(int id);
    }
}
