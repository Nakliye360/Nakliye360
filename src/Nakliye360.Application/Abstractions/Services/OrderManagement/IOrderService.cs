using Nakliye360.Application.Models.DTOs.OrderManagement;

namespace Nakliye360.Application.Abstractions.Services.OrderManagement
{
    /// <summary>
    /// Defines the operations for working with orders.
    /// </summary>
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllAsync();
        Task<OrderDto?> GetByIdAsync(int id);
        Task<int> CreateOrderAsync(CreateOrderDto dto);
        Task<string> UpdateOrderAsync(UpdateOrderDto dto);
        Task DeleteOrderAsync(int id);
    }
}
