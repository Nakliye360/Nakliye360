using Nakliye360.Domain.Enums;

namespace Nakliye360.Application.Models.DTOs.OrderManagement
{
    /// <summary>
    /// DTO representing an order with its items.
    /// </summary>
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
