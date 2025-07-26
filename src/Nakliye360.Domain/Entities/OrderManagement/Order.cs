using Nakliye360.Domain.Enums;

namespace Nakliye360.Domain.Entities.OrderManagement
{
    /// <summary>
    /// Represents a shipment or delivery order.
    /// </summary>
    public class Order : BaseEntity
    {
        /// <summary>
        /// Identifier of the customer who placed the order.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Date when the order was created.
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Current status of the order.
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Collection of order items.
        /// </summary>
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
