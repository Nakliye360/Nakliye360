using Nakliye360.Application.Models.DTOs.ShipmentManagement;

namespace Nakliye360.Application.Abstractions.Services.ShipmentManagement
{
    /// <summary>
    /// Abstraction of a shipment service that provides CRUD operations for shipments.
    /// </summary>
    public interface IShipmentService
    {
        Task<List<ShipmentDto>> GetAllAsync();
        Task<ShipmentDto?> GetByIdAsync(int id);
        Task<int> CreateShipmentAsync(CreateShipmentDto dto);
        Task<string> UpdateShipmentAsync(UpdateShipmentDto dto);
        Task DeleteShipmentAsync(int id);
    }
}