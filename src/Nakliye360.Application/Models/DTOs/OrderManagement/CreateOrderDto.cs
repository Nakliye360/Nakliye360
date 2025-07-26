using System.ComponentModel.DataAnnotations;

namespace Nakliye360.Application.Models.DTOs.OrderManagement
{
    /// <summary>
    /// DTO used to create a new order.
    /// </summary>
    public class CreateOrderDto
    {
        [Required]
        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
