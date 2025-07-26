using Nakliye360.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Nakliye360.Application.Models.DTOs.OrderManagement
{
    /// <summary>
    /// DTO used to update an existing order.
    /// </summary>
    public class UpdateOrderDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderStatus Status { get; set; }

        public List<OrderItemDto> Items { get; set; } = new();
    }
}
