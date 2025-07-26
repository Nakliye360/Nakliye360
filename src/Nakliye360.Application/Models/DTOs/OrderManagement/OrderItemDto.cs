namespace Nakliye360.Application.Models.DTOs.OrderManagement
{
    /// <summary>
    /// DTO representing a single item in an order.
    /// </summary>
    public class OrderItemDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal? Weight { get; set; }
    }
}
