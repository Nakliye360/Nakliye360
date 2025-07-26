namespace Nakliye360.Domain.Entities.OrderManagement
{
    /// <summary>
    /// Represents an individual item within an order.
    /// </summary>
    public class OrderItem : BaseEntity
    {
        /// <summary>
        /// Foreign key to the parent order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Name or description of the item being shipped.
        /// In a real application this could reference a Product entity.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Quantity of the item.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Optional weight of the item for freight calculations.
        /// </summary>
        public decimal? Weight { get; set; }
    }
}
